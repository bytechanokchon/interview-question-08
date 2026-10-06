namespace Application.Dtos.Requests
{
    public class ResultOptionUpdateRequestDto
    {
        public int? Id { get; set; }
        public required string Title { get; set; }
        public required bool IsCorrect { get; set; }
        public required int RowState { get; set; }
    }
}
