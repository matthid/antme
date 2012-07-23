using System;

namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Holds information about sugar.
    /// </summary>
    [Serializable]
    public class SugarState : IndexBasedState {
        #region internal Variables

        /// <summary>
        /// Amount of food.
        /// </summary>
        private int m_amount;

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
        /// Constructor of sugar-state
        /// </summary>
        /// <param name="id">id</param>
        public SugarState(int id) : base(id) {}

        #region Properties

        /// <summary>
        /// Gets or sets the load of sugar.
        /// </summary>
        public int Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }

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

        #endregion
    }
}