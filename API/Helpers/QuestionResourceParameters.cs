namespace API.Helpers
{
    /// <summary>
    /// Class representive of parameters to filter/pagining a question
    /// </summary>
    public class QuestionResourceParameters
    {
        private const int MAX_LIMIT = 20;
        private int _limit = 10;

        /// <summary>
        /// Number of the page
        /// </summary>
        public int Offset { get; set; } = 1;

        /// <summary>
        /// Number of the register per consult
        /// </summary>
        public int Limit
        {
            get => _limit;
            set => _limit = (value > MAX_LIMIT) ? MAX_LIMIT : value;
        }

        /// <summary>
        /// Field with the filters passeds on the API Call
        /// </summary>
        public string Filter { get; set; }
    }
}
