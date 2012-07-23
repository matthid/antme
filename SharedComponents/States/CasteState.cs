using System;

namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Holds information about ant-castes
    /// </summary>
    [Serializable]
    public class CasteState : ColonyBasedState {
        #region internal Variables

        /// <summary>
        /// Attack-modificator.
        /// </summary>
        private byte m_attackModificator;

        /// <summary>
        /// Load-modificator.
        /// </summary>
        private byte m_loadModificator;

        /// <summary>
        /// Name of caste.
        /// </summary>
        private string m_name;

        /// <summary>
        /// Range-modificator.
        /// </summary>
        private byte m_rangeModificator;

        /// <summary>
        /// Rotation-speed-modificator.
        /// </summary>
        private byte m_rotationSpeedModificator;

        /// <summary>
        /// Speed-modificator.
        /// </summary>
        private byte m_speedModificator;

        /// <summary>
        /// View-range-modificator.
        /// </summary>
        private byte m_viewRangeModificator;

        /// <summary>
        /// Vitality-modificator.
        /// </summary>
        private byte m_vitalityModificator;

        #endregion

        /// <summary>
        /// Constructor of caste-state
        /// </summary>
        /// <param name="colonyId">Id of colony</paraparam>
        /// <param name="id">Id of caste</param>
        public CasteState(int colonyId, int id) : base(colonyId, id) {}

        #region Properties

        /// <summary>
        /// Gets or sets the attack-modificator.
        /// </summary>
        public byte AttackModificator {
            get { return m_attackModificator; }
            set { m_attackModificator = value; }
        }

        /// <summary>
        /// Gets or sets the load-modificator.
        /// </summary>
        public byte LoadModificator {
            get { return m_loadModificator; }
            set { m_loadModificator = value; }
        }

        /// <summary>
        /// Gets or sets the name of this caste.
        /// </summary>
        public string Name {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or sets the range-modificator.
        /// </summary>
        public byte RangeModificator {
            get { return m_rangeModificator; }
            set { m_rangeModificator = value; }
        }

        /// <summary>
        /// Gets or sets the rotation-speed-modificator.
        /// </summary>
        public byte RotationSpeedModificator {
            get { return m_rotationSpeedModificator; }
            set { m_rotationSpeedModificator = value; }
        }

        /// <summary>
        /// Gets or sets the speed-modificator.
        /// </summary>
        public byte SpeedModificator {
            get { return m_speedModificator; }
            set { m_speedModificator = value; }
        }

        /// <summary>
        /// Gets or sets the view-range-modificator.
        /// </summary>
        public byte ViewRangeModificator {
            get { return m_viewRangeModificator; }
            set { m_viewRangeModificator = value; }
        }

        /// <summary>
        /// Gets or sets the vitality-modificator.
        /// </summary>
        public byte VitalityModificator {
            get { return m_vitalityModificator; }
            set { m_vitalityModificator = value; }
        }

        #endregion
    }
}