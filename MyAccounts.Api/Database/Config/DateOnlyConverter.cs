﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MyAccounts.Api.Database.Config
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base (
                d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d)
            )
        { }
    }
}
