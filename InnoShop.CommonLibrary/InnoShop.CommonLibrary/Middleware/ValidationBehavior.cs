﻿using FluentValidation;
using InnoShop.CommonLibrary.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.CommonLibrary.Middleware
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();
             
            var context = new ValidationContext<TRequest>(request);

            var errorsDictionary = _validators
                .Select(ed => ed.Validate(context))
                .SelectMany(ed => ed.Errors)
                .Where(ed => ed is not null)
                .GroupBy(
                    ed => ed.PropertyName.Substring(ed.PropertyName.IndexOf('.') + 1), 
                    ed => ed.ErrorMessage, (propertyName, errorMessage) => new
                    {
                        Key = propertyName,
                        Values = errorMessage.Distinct().ToArray()
                    })
                .ToDictionary(ed => ed.Key, ed => ed.Values);
            
            if (errorsDictionary.Any())
                throw new ValidationAppException(errorsDictionary);

            return await next();
        }
    }
}