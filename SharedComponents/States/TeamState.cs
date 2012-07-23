using System;
using System.Collections.ObjectModel;

namespace AntMe.SharedComponents.States
{
    /// <summary>
    /// Holds the information of a team of multiple colonies.
    /// </summary>
    [Serializable]
    public class TeamState : IndexBasedState
    {
        #region internal Variables

        /// <summary>
        /// <c>Guid</c> of team.
        /// </summary>
        private Guid m_guid;

        /// <summary>
        /// Team-name.
        /// </summary>
        private string m_name;

        #endregion

        #region internal lists

        private readonly Collection<ColonyState> colonyStates;

        #endregion

        /// <summary>
        /// Constructor of team-state
        /// </summary>
        /// <param name="id">id</param>
        public TeamState(int id)
            : base(id)
        {
            colonyStates = new Collection<ColonyState>();
        }

        /// <summary>
        /// Constructor of team-state
        /// </summary>
        /// <param name="id">id of this team</param>
        /// <param name="guid"><c>guid</c></param>
        /// <param name="name">Name of this team</param>
        public TeamState(int id, Guid guid, string name)
            : this(id)
        {
            m_guid = guid;
            m_name = name;
        }

        #region Properties

        /// <summary>
        /// gets a list of castes.
        /// </summary>
        public Collection<ColonyState> ColonyStates
        {
            get { return colonyStates; }
        }

        /// <summary>
        /// Gets or sets the <c>guid</c> of the team.
        /// </summary>
        public Guid Guid
        {
            get { return m_guid; }
            set { m_guid = value; }
        }

        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        #endregion
    }
}