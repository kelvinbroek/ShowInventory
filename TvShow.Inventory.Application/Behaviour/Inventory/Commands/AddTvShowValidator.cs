using FluentValidation;
using TvShow.Inventory.Application.Models;

namespace TvShow.Inventory.Application.Behaviour.Inventory.Commands
{
    public class AddTvShowValidator : AbstractValidator<AddTvShowVM>
    {
        public AddTvShowValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleForEach(x => x.Genres).SetValidator(new AddTvShowGenreValidator());
        }
    }

    public class AddTvShowGenreValidator : AbstractValidator<GenreDto>
    {
        public AddTvShowGenreValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Genre Id cannot be empty");
        }
    }
}
