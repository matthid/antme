using System;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.DirectX;

namespace AntMe.Plugin.Mdx {
    /// <summary>
    /// Represents a camera
    /// </summary>
    internal sealed class Camera : IDisposable {
        #region Constants

        private const int DISTANCE_MAX = 12000;
        private const int SCROLLDISTANCE_MAX = 1000;
        private const float CAMERAANGLE_MAX = ((float) Math.PI/2) - 0.01f;
        private const int DISTANCE_MIN = 100;
        private const float CAMERAANGLE_MIN = 0.1f;

        #endregion

        #region Variables

        private Vector3 viewerCenter;
        private bool moveArea;
        private bool hasFocus;
        private Vector3 cameraDirection;
        private Vector3 cameraUpvector;
        private Vector3 cameraPosition;

        private int mouseX;
        private int mouseY;
        private int distanceMax;
        private bool rotateCamera;
        private RenderForm renderForm;

        #endregion

        #region Construction and init

        /// <summary>
        /// Creates a new instance of camera
        /// </summary>
        /// <param name="renderForm">render-Form</param>
        public Camera(RenderForm renderForm) {
            // Attach form-events for interaction
            this.renderForm = renderForm;
            this.renderForm.MouseEnter += form_mouseEnter;
            this.renderForm.MouseLeave += form_mouseLeave;
            this.renderForm.MouseDown += form_mouseDown;
            this.renderForm.MouseUp += form_mouseUp;
            this.renderForm.MouseMove += form_mouseMove;
            this.renderForm.MouseWheel += form_mouseWheel;

            // Reset Camera-position
            viewerCenter = new Vector3(0, 2, 0);
            cameraPosition = new Vector3(0, DISTANCE_MAX, 0);
            cameraUpvector = new Vector3(0, 1, 1);
            cameraDirection =
                new Vector3(((float) Math.PI*3)/2, CAMERAANGLE_MAX, DISTANCE_MAX);
            cameraUpvector.Normalize();
        }

        #endregion

        #region Form-Events

        private void form_mouseWheel(object sender, MouseEventArgs e) {
            if (hasFocus) {
                // calculate delta
                cameraDirection.Z -= (e.Delta/5);

                // check distance-limits
                if (cameraDirection.Z < DISTANCE_MIN) {
                    cameraDirection.Z = DISTANCE_MIN;
                }
                else if (cameraDirection.Z > DISTANCE_MAX) {
                    cameraDirection.Z = DISTANCE_MAX;
                }
            }
        }

        private void form_mouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                moveArea = false;
            }
            else if (e.Button == MouseButtons.Right) {
                rotateCamera = false;
            }
        }

        private void form_mouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                moveArea = true;
            }
            else if (e.Button == MouseButtons.Right) {
                rotateCamera = true;
            }
        }

        private void form_mouseLeave(object sender, EventArgs e) {
            hasFocus = false;
            mouseX = -1;
            mouseY = -1;
            rotateCamera = false;
            moveArea = false;
        }

        private void form_mouseEnter(object sender, EventArgs e) {
            hasFocus = true;
            renderForm.Focus();
        }

        private void form_mouseMove(object sender, MouseEventArgs e) {
            if (hasFocus) {
                if (mouseX == -1) {
                    mouseX = e.X;
                    mouseY = e.Y;
                }

                // calculate deltas
                int deltaX = e.X - mouseX;
                int deltaY = e.Y - mouseY;

                // calculate movement
                if (moveArea) {
                    float sinX = (float) Math.Sin(cameraDirection.X);
                    float cosX = (float) Math.Cos(cameraDirection.X);
                    viewerCenter.X += sinX*deltaX;
                    viewerCenter.X -= cosX*deltaY;
                    viewerCenter.Z -= cosX*deltaX;
                    viewerCenter.Z -= sinX*deltaY;

                    // check scrolling-limits
                    if (viewerCenter.X < -SCROLLDISTANCE_MAX) {
                        viewerCenter.X = -SCROLLDISTANCE_MAX;
                    }
                    else if (viewerCenter.X > SCROLLDISTANCE_MAX) {
                        viewerCenter.X = SCROLLDISTANCE_MAX;
                    }

                    if (viewerCenter.Z < -SCROLLDISTANCE_MAX) {
                        viewerCenter.Z = -SCROLLDISTANCE_MAX;
                    }
                    else if (viewerCenter.Z > SCROLLDISTANCE_MAX) {
                        viewerCenter.Z = SCROLLDISTANCE_MAX;
                    }
                }

                // calculate rotation
                if (rotateCamera) {
                    cameraDirection.X += (float) deltaX/1000;
                    cameraDirection.Y += (float) deltaY/1000;

                    // check, rotationlimits
                    if (cameraDirection.Y < CAMERAANGLE_MIN) {
                        cameraDirection.Y = CAMERAANGLE_MIN;
                    }
                    else if (cameraDirection.Y > CAMERAANGLE_MAX) {
                        cameraDirection.Y = CAMERAANGLE_MAX;
                    }
                }

                // save new mouse-position
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        #endregion

        /// <summary>
        /// Gets the current View-Matrix.
        /// </summary>
        public Matrix ViewMatrix {
            get { return Matrix.LookAtLH(cameraPosition, viewerCenter, cameraUpvector); }
        }

        #region Properties

        /// <summary>
        /// Gets the current Pickray.
        /// </summary>
        public Pickray Pickray {
            get {
                if (mouseX != -1) {
                    Matrix projektionsMatrix = renderForm.ProjectionMatrix;
                    Pickray outputRay = new Pickray();

                    // create ray
                    outputRay.Origin = new Vector3(0.0f, 0.0f, 0.0f);
                    outputRay.Direction =
                        new Vector3
                            (
                            (((2.0f*mouseX)/renderForm.ClientSize.Width) - 1.0f)/projektionsMatrix.M11,
                            (((-2.0f*mouseY)/renderForm.ClientSize.Height) + 1.0f)/projektionsMatrix.M22,
                            1.0f);

                    // tranform ray to view
                    Matrix viewMatrix = ViewMatrix;
                    viewMatrix.Invert();
                    outputRay.Origin.TransformCoordinate(viewMatrix);
                    outputRay.Direction.TransformNormal(viewMatrix);
                    outputRay.Direction.Normalize();

                    return outputRay;
                }
                else {
                    // Empty ray, if there is no mouse
                    return new Pickray();
                }
            }
        }

        /// <summary>
        /// Gets the current Mouse-Position.
        /// </summary>
        public Point MousePosition {
            get {
                if (mouseX != -1) {
                    return new Point(mouseX, mouseY);
                }
                else {
                    return new Point(0, 0);
                }
            }
        }

        /// <summary>
        /// Gets the current Camera-Position.
        /// </summary>
        public Vector3 CameraPosition {
            get { return cameraPosition; }
        }

        /// <summary>
        /// Disposes all resources.
        /// </summary>
        public void Dispose() {
            renderForm.MouseEnter -= form_mouseEnter;
            renderForm.MouseLeave -= form_mouseLeave;
            renderForm.MouseDown -= form_mouseDown;
            renderForm.MouseUp -= form_mouseUp;
            renderForm.MouseMove -= form_mouseMove;
            renderForm.MouseWheel -= form_mouseWheel;

            renderForm = null;
        }

        #endregion

        public void Update(int playgroundWidth, int playgroundHeight) {
            // Maximale Distanz neu ermitteln
            distanceMax = playgroundWidth;

            // Maximalentfernung korrigieren
            if (cameraDirection.Z > distanceMax) {
                cameraDirection.Z = distanceMax;
            }

            // Camerapos ermitteln
            float distance = (float) (Math.Cos(cameraDirection.Y)*cameraDirection.Z);
            cameraPosition.Y = (float) (Math.Sin(cameraDirection.Y)*cameraDirection.Z);
            cameraPosition.Z = (float) (Math.Sin(cameraDirection.X)*distance) + viewerCenter.Z;
            cameraPosition.X = (float) (Math.Cos(cameraDirection.X)*distance) + viewerCenter.X;

            // Upvector ermitteln
            cameraUpvector.X = cameraPosition.X - viewerCenter.X;
            cameraUpvector.Z = cameraPosition.Z - viewerCenter.Z;
            cameraUpvector.Y = cameraPosition.Y;
            cameraUpvector.Normalize();
        }
    }
}