using HomeWard.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWard.Application;

public sealed record Result(bool sucess, string? errorMessage = null);
