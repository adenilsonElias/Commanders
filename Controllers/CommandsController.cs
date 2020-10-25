using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> getAllCommands()
        {
            var commandsItems = _repository.getAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandsItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "getCommandById")]
        public ActionResult<CommandReadDto> getCommandById(int id)
        {
            var commandItem = _repository.getCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();

        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> createCommands(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.createCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(getCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        //PUT api/commands/{id}

        [HttpPut("{id}")]

        public ActionResult<CommandReadDto> updateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.getCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            _repository.updateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return _mapper.Map<CommandReadDto>(commandModelFromRepo);
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult partialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.getCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.updateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        //DELETE api/commands/{id}

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.getCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.deleteCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}