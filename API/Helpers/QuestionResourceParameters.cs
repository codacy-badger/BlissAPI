namespace API.Helpers
{
    /// <summary>
    /// Class representive of parameters to filter/pagining a question
    /// </summary>
    public class QuestionResourceParameters
    {
        const int maxLimit = 20;

        public int Offset { get; set; } = 1;

        private int _limit = 10;

        public int Limit
        {
            get => _limit;
            set => _limit = (value > maxLimit) ? maxLimit : value;
        }

        public string Filter { get; set; }
    }
}
