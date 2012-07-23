using System;
using System.Collections.ObjectModel;

namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Holds the information of one colony in a simulation-state.
    /// </summary>
    [Serializable]
    public class ColonyState : IndexBasedState {
        #region internal Variables

        /// <summary>
        /// <c>Guid</c> of that colony.
        /// </summary>
        private Guid m_guid;

        /// <summary>
        /// Count of beaten ants.
        /// </summary>
        private int m_beatenAnts;

        /// <summary>
        /// Count of collected food.
        /// </summary>
        private int m_collectedFood;

        /// <summary>
        /// Count of collected fruits.
        /// </summary>
        private int m_collectedFruits;

        /// <summary>
        /// Count of eaten ants.
        /// </summary>
        private int m_eatenAnts;

        /// <summary>
        /// Colony-name.
        /// </summary>
        private string m_colonyName;

        /// <summary>
        /// Count of killed bugs.
        /// </summary>
        private int m_killedBugs;

        /// <summary>
        /// Count of killed enemies.
        /// </summary>
        private int m_killedEnemies;

        /// <summary>
        /// Name of player.
        /// </summary>
        private string m_playerName;

        /// <summary>
        /// Total count of points.
        /// </summary>
        private int m_points;

        /// <summary>
        /// count of starved ants.
        /// </summary>
        private int m_starvedAnts;

        #endregion

        #region internal lists

        private readonly Collection<AnthillState> anthillStates;
        private readonly Collection<AntState> antStates;
        private readonly Collection<CasteState> casteStates;
        private readonly Collection<MarkerState> markerStates;

        #endregion

        /// <summary>
        /// Constructor of colony-state
        /// </summary>
        /// <param name="id">id</param>
        public ColonyState(int id) : base(id) {
            antStates = new Collection<AntState>();
            anthillStates = new Collection<AnthillState>();
            markerStates = new Collection<MarkerState>();
            casteStates = new Collection<CasteState>();
        }

        /// <summary>
        /// Constructor of colony-state
        /// </summary>
        /// <param name="id">id of this colony</param>
        /// <param name="guid"><c>guid</c></param>
        /// <param name="colonyName">Name of this colony</param>
        /// <param name="playerName">Name of player</param>
        public ColonyState(int id, Guid guid, string colonyName, string playerName)
            : this(id) {
            m_guid = guid;
            m_colonyName = colonyName;
            m_playerName = playerName;
        }

        #region Properties

        /// <summary>
        /// Gets a list of ants.
        /// </summary>
        public Collection<AntState> AntStates {
            get { return antStates; }
        }

        /// <summary>
        /// Gets a list of anthills.
        /// </summary>
        public Collection<AnthillState> AnthillStates {
            get { return anthillStates; }
        }

        /// <summary>
        /// Gets a list of markers.
        /// </summary>
        public Collection<MarkerState> MarkerStates {
            get { return markerStates; }
        }

        /// <summary>
        /// gets a list of castes.
        /// </summary>
        public Collection<CasteState> CasteStates {
            get { return casteStates; }
        }

        /// <summary>
        /// Gets or sets the guid of the colony.
        /// </summary>
        public Guid Guid
        {
            get { return m_guid; }
            set { m_guid = value; }
        }

        /// <summary>
        /// Gets or sets the name of this colony.
        /// </summary>
        public string ColonyName {
            get { return m_colonyName; }
            set { m_colonyName = value; }
        }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string PlayerName {
            get { return m_playerName; }
            set { m_playerName = value; }
        }

        /// <summary>
        /// Gets or sets the count of starved ants.
        /// </summary>
        public int StarvedAnts {
            get { return m_starvedAnts; }
            set { m_starvedAnts = value; }
        }

        /// <summary>
        /// Gets or sets the count of eaten ants.
        /// </summary>
        public int EatenAnts {
            get { return m_eatenAnts; }
            set { m_eatenAnts = value; }
        }

        /// <summary>
        /// Gets or sets the count of beaten ants.
        /// </summary>
        public int BeatenAnts {
            get { return m_beatenAnts; }
            set { m_beatenAnts = value; }
        }

        /// <summary>
        /// Gets or sets the count of killed bugs.
        /// </summary>
        public int KilledBugs {
            get { return m_killedBugs; }
            set { m_killedBugs = value; }
        }

        /// <summary>
        /// Gets or sets the count of killed enemies.
        /// </summary>
        public int KilledEnemies {
            get { return m_killedEnemies; }
            set { m_killedEnemies = value; }
        }

        /// <summary>
        /// Gets or sets the amount of collected food.
        /// </summary>
        public int CollectedFood {
            get { return m_collectedFood; }
            set { m_collectedFood = value; }
        }

        /// <summary>
        /// Gets or sets the amount of collected fruits.
        /// </summary>
        public int CollectedFruits {
            get { return m_collectedFruits; }
            set { m_collectedFruits = value;}
        }

        /// <summary>
        /// Gets or sets the total points.
        /// </summary>
        public int Points {
            get { return m_points; }
            set { m_points = value; }
        }

        #endregion
    }
}