using System;

namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Holds information about a marker.
    /// </summary>
    [Serializable]
    public class MarkerState : ColonyBasedState {
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
        /// Radius.
        /// </summary>
        private int m_radius;

        #endregion

        /// <summary>
        /// Constructor of marker-state.
        /// </summary>
        /// <param name="colonyId">Colony-id</param>
        /// <param name="id">id</param>
        public MarkerState(int colonyId, int id) : base(colonyId, id) {}

        #region Properties

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
        /// Gets or sets the radius.
        /// </summary>
        public int Radius
        {
            get { return m_radius; }
            set { m_radius = value; }
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public int Direction
        {
            get { return m_direction; }
            set { m_direction = value; }
        }

        #endregion
    }
}