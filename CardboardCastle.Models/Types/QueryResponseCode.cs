﻿namespace CardboardCastle.Models.Types
{
    public enum QueryResponseCode
    {
        Default = 0,
        //Informational
        Continue = 100,
        Switching = 101,
        Processing = 102,
        EarlyHints = 103,
        //Success
        Ok = 200,
        Created = 201,
        Accepted = 202,
        ConformedResponse = 203,
        NoContent = 204,
        ResetContent = 205,
        PartialContent = 206,
        //Redirection
        MultipleChoices = 300,
        Moved = 301,
        Found = 302,
        SeeOther = 303,
        NotModified = 304,
        UseProxy = 305,
        SwitchProxy = 306,
        TemporaryRedirect = 307,
        PerminantRedirect = 308,
        //Client Errors
        BadRequest = 400,
        Unauthroized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        NotAllowed = 405,
        NotAcceptable = 406,
        RequestTimedout = 408,
        Conflict = 409,
        Gone = 410,
        PreconditionFailed = 412,
        PayloadTooLarge = 413,
        UriTooLong = 414,
        Unsupported = 415,
        ExpectationFailed = 417,
        Teapot = 418,
        MisdirectedRequest = 421,
        Locked = 423,
        FailedDepedency = 424,
        UpgradeRequired = 426,
        TooManyRequests = 429,
        DeniedForLegalReasons = 451,
        //Server Errors
        Error = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        VersionNotSupported = 505,
        CircularReference = 506,
        InsufficientStorage = 507,
        LoopDetected = 508,
        NotExtended = 510,
        AuthenticationRequired = 511
    }
}
