using System;
namespace AntMe.SharedComponents.States {
    /// <summary>
    /// Base-class for all index-based states
    /// </summary>
    [Serializable]
    public abstract class IndexBasedState : IComparable<IndexBasedState> {

        /// <summary>
        /// ID of this state.
        /// </summary>
        private int m_id;

        /// <summary>
        /// Constructor of this state.
        /// </summary>
        /// <param name="id"></param>
        public IndexBasedState(int id) {
            m_id = id;
        }

        /// <summary>
        /// Gets the id of this state.
        /// </summary>
        public int Id {
            get { return m_id; }
            protected set { m_id = value; }
        }

        #region IComparable<IndexBasedState> Member

        /// <summary>
        /// Compares two IndexBasedStates
        /// </summary>
        /// <param name="other">other state</param>
        /// <returns>compare-result</returns>
        public int CompareTo(IndexBasedState other)
        {
            return Id.CompareTo(other.Id);
        }

        #endregion
    }
}