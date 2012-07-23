using System;

namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Holds information about an ant.
    /// </summary>
    [Serializable]
    public class AntState : ColonyBasedState {
        #region internal Variables

        /// <summary>
        /// ID of caste.
        /// </summary>
        private int m_casteId;

        /// <summary>
        /// Direction.
        /// </summary>
        private int m_direction;

        /// <summary>
        /// Load.
        /// </summary>
        private int m_load;

        /// <summary>
        /// Target.
        /// </summary>
        private TargetType m_targetType;

        /// <summary>
        /// Type of load.
        /// </summary>
        private LoadType m_loadType;

        /// <summary>
        /// x-part of target position.
        /// </summary>
        private int m_targetPositionX;

        /// <summary>
        /// y-part of target position.
        /// </summary>
        private int m_targetPositionY;

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

        #region Constructor

        /// <summary>
        /// Constructor of ant-state
        /// </summary>
        /// <param name="colonyId">Colony-id</param>
        /// <param name="id">id</param>
        public AntState(int colonyId, int id) : base(colonyId, id) {
            m_loadType = LoadType.None;
            m_targetType = TargetType.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the id of the caste.
        /// </summary>
        public int CasteId
        {
            get { return m_casteId; }
            set { m_casteId = value; }
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
        /// Gets or sets the load.
        /// </summary>
        public int Load
        {
            get { return m_load; }
            set { m_load = value; }
        }

        /// <summary>
        /// Gets or sets the type of load.
        /// </summary>
        public LoadType LoadType {
            get { return m_loadType; }
            set { m_loadType = value; }
        }

        /// <summary>
        /// Gets or sets the x-part of position.
        /// </summary>
        public int PositionX
        {
            get { return m_positionX; }
            set { m_positionX = value; }
        }

        /// <summary>
        /// Gets or sets the kind of target.
        /// </summary>
        public TargetType TargetType {
            get { return m_targetType; }
            set { m_targetType = value; }
        }

        /// <summary>
        /// Gets or sets the x-part of the target position.
        /// </summary>
        public int TargetPositionX
        {
            get { return m_targetPositionX; }
            set { m_targetPositionX = value; }
        }

        /// <summary>
        /// Gets or sets the y-part of the target position.
        /// </summary>
        public int TargetPositionY
        {
            get { return m_targetPositionY; }
            set { m_targetPositionY = value; }
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
        /// Gets or sets the vitality.
        /// </summary>
        public int Vitality
        {
            get { return m_vitality; }
            set { m_vitality = value; }
        }

        #endregion
    }
}