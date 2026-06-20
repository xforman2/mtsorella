using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Highlights;

namespace Mtsorella.Api.Tests.Domain.Highlights;

public class MonthlyHighlightTests
{
    [Fact]
    public void Create_valid_highlight_succeeds()
    {
        var result = MonthlyHighlight.Create(new YearMonth(2026, 6), MemberId.New(), "Outstanding effort");

        Assert.False(result.IsError);
    }

    [Fact]
    public void Create_rejects_invalid_month()
    {
        var result = MonthlyHighlight.Create(new YearMonth(2026, 13), MemberId.New(), "x");

        Assert.True(result.IsError);
    }
}
