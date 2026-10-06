namespace Application.Dtos.Requests
{
    public class QuestionUpdateRequestDto
    {
        public int? Id { get; set; }
        public required string Title { get; set; }
        public required int RowState { get; set; }
        public required List<ResultOptionUpdateRequestDto> ResultOptions { get; set; }
    }
}
