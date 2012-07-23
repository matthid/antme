using System;

namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Holds information about bugs.
    /// </summary>
    [Serializable]
    public class BugState : IndexBasedState {
        #region internal Variables

        /// <summary>
        /// Direction.
        /// </summary>
        private int m_direction;

        /// <summary>
        /// x-part of position.
        /// </summary>
        private int m_positionX;

        /// <summary>
        /// y-part of position.
        /// </summary>
        private int m_positionY;

        /// <summary>
        /// Vitality.
        /// </summary>
        private int m_vitality;

        #endregion

        /// <summary>
        /// Constructor of bugstate.
        /// </summary>
        /// <param name="id">id</param>
        public BugState(int id) : base(id) {}

        /// <summary>
        /// Gets or sets the x-part of the position.
        /// </summary>
        public int PositionX
        {
            get { return m_positionX; }
            set { m_positionX = value; }
        }

        /// <summary>
        /// Gets or sets the y-part of the position.
        /// </summary>
        public int PositionY
        {
            get { return m_positionY; }
            set { m_positionY = value; }
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public int Direction
        {
            get { return m_direction; }
            set { m_direction = value; }
        }

        /// <summary>
        /// Gets or sets the vitality.
        /// </summary>
        public int Vitality
        {
            get { return m_vitality; }
            set { m_vitality = value; }
        }
    }
}