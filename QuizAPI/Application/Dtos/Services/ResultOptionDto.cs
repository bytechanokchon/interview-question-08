namespace Application.Dtos.Services
{
    public class ResultOptionDto
    {
        public int? Id { get; set; }
        public required string Title { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
