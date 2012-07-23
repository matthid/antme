using System;

namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Holds information about an anthill.
    /// </summary>
    [Serializable]
    public class AnthillState : ColonyBasedState {
        #region internal Variables

        /// <summary>
        /// x-part of position.
        /// </summary>
        private int m_positionX;

        /// <summary>
        /// y-part of position.
        /// </summary>
        private int m_positionY;

        /// <summary>
        /// radius
        /// </summary>
        private int m_radius;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of anthill-state
        /// </summary>
        /// <param name="colonyId">Colony-id</param>
        /// <param name="id">id</param>
        public AnthillState(int colonyId, int id) : base(colonyId, id) {}

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the x-part of position.
        /// </summary>
        public int PositionX
        {
            get { return m_positionX; }
            set { m_positionX = value; }
        }

        /// <summary>
        /// Gets or sets the y-part of position.
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

        #endregion
    }
}