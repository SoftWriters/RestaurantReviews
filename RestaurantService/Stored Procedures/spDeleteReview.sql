CREATE  PROCEDURE spDeleteCustomerReview
						@iReviewID int
AS
/****************************************************************************
	Syntax: spDeleteReview
	Purpose: Deletes customer review for Restaurant based on ReviewId
	Updates: 07/22/2015*****************************************************************************/

set xact_abort on

-- delete the customer review
delete CustomerReview
where ReviewId = @iReviewId

set xact_abort off

return