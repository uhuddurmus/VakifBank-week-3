﻿using FluentValidation;
using System;
using WebApi.BookOperations.UpdateBook;

namespace BookStore.BookOperations.UpdateBook;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(3);
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
    }
}