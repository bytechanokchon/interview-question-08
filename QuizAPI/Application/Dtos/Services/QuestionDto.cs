namespace Application.Dtos.Services
{
    public class QuestionDto
    {
        public int? Id { get; set; }
        public required string Title { get; set; }
        public required List<ResultOptionDto> ResultOptions { get; set; }
    }
}
