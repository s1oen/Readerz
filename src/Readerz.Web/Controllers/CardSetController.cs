using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reader.Application.Cards.Commands.DeleteCard;
using Reader.Application.CardSets.Commands.CreateCardSet;
using Reader.Application.CardSets.Commands.DeleteCardSet;
using Reader.Application.CardSets.Commands.UpdateCardSet;
using Reader.Application.CardSets.Queries.GetCardSetDetail;
using Reader.Application.CardSets.Queries.GetCardSets;
using Reader.Application.Common.Models;

namespace Readerz.Web.Controllers
{
    [Authorize]
    public class CardSetController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateCardSetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateCardSetCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteCardCommand>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCardSetCommand { CardSetId = id}));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PaginatorResult<CardSetDto>>> Get(int pageIndex = 0, int pageSize = 10,
            bool byCurrentUser = false)
        {
            return Ok(await Mediator.Send(new GetCardSetsQuery
            {
                PageIndex = pageIndex, 
                PageSize = pageSize, 
                ByCurrentUser = byCurrentUser
            }));
        }

        [HttpGet]
        public async Task<ActionResult<CardSetDetailDto>> GetDetail(int cardSetId)
        {
            return await Mediator.Send(new GetCardSetDetailQuery {CardSetId = cardSetId});
        }
    }
}