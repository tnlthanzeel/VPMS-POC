using FluentValidation.Results;
using Newtonsoft.Json;
using System.Net;
using VPMS.Application.Exceptions;

namespace VPMS.Application.Responses;

public sealed class ResponseResult : ResponseResult<object>
{
    public ResponseResult() : base(default(object))
    {
        Success = true;
    }

    public ResponseResult(IList<ValidationFailure> validationFailures) : base(default(object))
    {
        HttpStatusCode = HttpStatusCode.BadRequest;

        if (validationFailures.Count is not 0)
        {
            Errors.AddRange(validationFailures.Select(s => new KeyValuePair<string, IEnumerable<string>>(s.PropertyName, new[] { s.ErrorMessage })).ToList());
        }
    }

    public ResponseResult(ApplicationException ex) : base(default(object))
    {
        var errorMsg = new[] { ex.Message };

        switch (ex)
        {
            case BadRequestException e:
                HttpStatusCode = HttpStatusCode.BadRequest;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName, errorMsg));
                break;

            case ValidationException e:
                HttpStatusCode = HttpStatusCode.BadRequest;
                Errors.AddRange(e.ValdationErrors);
                break;

            case NotFoundException e:
                HttpStatusCode = HttpStatusCode.NotFound;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName, errorMsg));
                break;

        };
    }

    [JsonIgnore]
    public override List<KeyValuePair<string, IEnumerable<string>>> Errors { get; init; } = new();
}
