using NUnit.Framework;
using DTOs;
using DTOs.Validators;

namespace UnitTests
{
  public class ReviewValidatorTests
  {
    private ReviewValidator _sut;

    [SetUp]
    public void Setup()
    {
      _sut = new ReviewValidator();
    }

    [Test]
    public void HappyPath()
    {
      var poco = new ReviewDTO { Stars = 3, ReviewText = "This is a review that I wrote!" };
      Assert.DoesNotThrow(() => _sut.ValidateData(poco));
    }

    [Test]
    public void InvalidStarRating_ThrowsException()
    {
      var poco = new ReviewDTO { Stars = -1, ReviewText = "This is a review that I wrote!" };
      Assert.Throws<ValidationException>(() => _sut.ValidateData(poco));
    }

    [Test]
    public void ReviewTextTooSmall_ThrowsException()
    {
      var poco = new ReviewDTO { Stars = 2, ReviewText = "Too Lazy To Write" };
      Assert.Throws<ValidationException>(() => _sut.ValidateData(poco));
    }
  }
}