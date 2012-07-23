using System;
using System.Collections.ObjectModel;

namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Holds the information of one single simulation-step
    /// </summary>
    [Serializable]
    public class SimulationState {
        #region internal Variables

        /// <summary>
        /// List of custom fields.
        /// </summary>
        private readonly CustomState m_customFields;

        /// <summary>
        /// Number of current round.
        /// </summary>
        private int m_currentRound;

        /// <summary>
        /// Height of playground.
        /// </summary>
        private int m_playgroundHeight;

        /// <summary>
        /// Width of playground.
        /// </summary>
        private int m_playgroundWidth;

        /// <summary>
        /// Time-stamp of this state.
        /// </summary>
        private DateTime m_timeStamp;

        /// <summary>
        /// Total count of rounds.
        /// </summary>
        private int m_totalRounds;

        #endregion

        #region internal Lists

        private readonly Collection<BugState> bugStates;
        private readonly Collection<TeamState> teamStates;
        private readonly Collection<FruitState> fruitStates;
        private readonly Collection<SugarState> sugarStates;

        #endregion

        #region Konstruction

        /// <summary>
        /// Constructor to initialize the lists.
        /// </summary>
        public SimulationState() {
            m_timeStamp = DateTime.Now;
            bugStates = new Collection<BugState>();
            fruitStates = new Collection<FruitState>();
            teamStates = new Collection<TeamState>();
            sugarStates = new Collection<SugarState>();
            m_customFields = new CustomState();
        }

        /// <summary>
        /// Constructor to initialize the lists and set the basic parameters.
        /// </summary>
        /// <param name="width">width of the playground</param>
        /// <param name="height">height of the playground</param>
        /// <param name="round">the current round</param>
        /// <param name="rounds">the number of total rounds</param>
        public SimulationState(int width, int height, int round, int rounds) :
            this() {
            m_playgroundWidth = width;
            m_playgroundHeight = height;
            m_currentRound = round;
            m_totalRounds = rounds;
        }

        /// <summary>
        /// Constructor to initialize the lists and set the basic parameters.
        /// </summary>
        /// <param name="width">width of the playground</param>
        /// <param name="height">height of the playground</param>
        /// <param name="round">the current round</param>
        /// <param name="rounds">the number of total rounds</param>
        /// <param name="time">the time-stamp of this simulation-state</param>
        public SimulationState(int width, int height, int round, int rounds, DateTime time) :
            this(width, height, round, rounds) {
            m_timeStamp = time;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of bugs.
        /// </summary>
        public Collection<BugState> BugStates {
            get { return bugStates; }
        }

        /// <summary>
        /// Gets a list of fruits.
        /// </summary>
        public Collection<FruitState> FruitStates {
            get { return fruitStates; }
        }

		public Collection<ColonyState> ColonyStates
		{
			get
			{
				Collection<ColonyState> colonies = new Collection<ColonyState>();
				foreach (TeamState team in teamStates)
					foreach (ColonyState colony in team.ColonyStates)
						colonies.Add(colony);
				return colonies;
			}
		}

        /// <summary>
        /// Gets a list of teams.
        /// </summary>
        public Collection<TeamState> TeamStates {
            get { return teamStates; }
        }

        /// <summary>
        /// Gets a list of sugar.
        /// </summary>
        public Collection<SugarState> SugarStates {
            get { return sugarStates; }
        }

        /// <summary>
        /// Gets the list of custom fields.
        /// </summary>
        public CustomState CustomFields {
            get { return m_customFields; }
        }

        /// <summary>
        /// Gets or sets the time-stamp of this simulation-state.
        /// </summary>
        public DateTime TimeStamp {
            get { return m_timeStamp; }
            set { m_timeStamp = value; }
        }

        /// <summary>
        /// Gets or sets the number of total rounds.
        /// </summary>
        public int TotalRounds {
            get { return m_totalRounds; }
            set { m_totalRounds = value; }
        }

        /// <summary>
        /// Gets or sets the number of current round.
        /// </summary>
        public int CurrentRound {
            get { return m_currentRound; }
            set { m_currentRound = value; }
        }

        /// <summary>
        /// Gets or sets the width of the playground.
        /// </summary>
        public int PlaygroundWidth {
            get { return m_playgroundWidth; }
            set { m_playgroundWidth = value; }
        }

        /// <summary>
        /// Gets or sets the height of the playground.
        /// </summary>
        public int PlaygroundHeight {
            get { return m_playgroundHeight; }
            set { m_playgroundHeight = value; }
        }

        #endregion
    }
}