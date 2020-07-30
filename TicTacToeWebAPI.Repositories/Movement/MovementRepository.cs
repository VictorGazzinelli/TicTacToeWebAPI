using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using TicTacToeWebAPI.Boundaries.Repositories;
using MovementEntity = TicTacToeWebAPI.Entities.Movement.Movement;

namespace TicTacToeWebAPI.Repositories.Movement
{
    public class MovementRepository : IMovementRepository
    {

        List<MovementEntity> list_Movements_In_Memory = new List<MovementEntity>();

        public string Create(MovementEntity movement)
        {
            string id = Guid.NewGuid().ToString();
            movement.id = id;
            movement.createdAt = DateTime.Now;

            return id;
        }

        public MovementEntity Retrieve(string id)
        {
            return list_Movements_In_Memory.Where(m => String.Equals(m.id, id))
                .FirstOrDefault();
        }
    }
}
