using System;

namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Holds information about fruit.
    /// </summary>
    [Serializable]
    public class FruitState : IndexBasedState {
        #region internal Variables

        /// <summary>
        /// Amount of food.
        /// </summary>
        private int m_amount;

        /// <summary>
        /// Count of carrying ants.
        /// </summary>
        private byte m_carryingAnts;

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
        /// Constructor of fruit-state.
        /// </summary>
        /// <param name="id">id</param>
        public FruitState(int id) : base(id) {}

        /// <summary>
        /// Gets or sets the amount of fruit.
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

        /// <summary>
        /// Gets or sets the number of carrying ants.
        /// </summary>
        public byte CarryingAnts {
            get { return m_carryingAnts; }
            set { m_carryingAnts = value; }
        }
    }
}