using System.Collections.Generic;
using SimpleAI.Utils;
//using GameContent;
//using GameContent.Agents;
//using GameContent.Defence;

namespace SimpleAI.Game
{
    /// <summary>
    /// Just recorder at present.
    /// </summary>
    public sealed class EntityManager : SingletonAsComponent<EntityManager>
    {
        private Dictionary<int, BaseGameEntity> EntityDic =
            new Dictionary<int, BaseGameEntity>();

        public List<BaseGameEntity> PlayerControlEntities =
            new List<BaseGameEntity>();

        public BaseGameEntity PlayerEntity = null;

        private EntityManager() { }

        static EntityManager() { }

        ~EntityManager()
        {
            Reset();
        }

        public static EntityManager Instance
        {
            get
            {
                return (EntityManager)InsideInstance;
            }
        }

        /// <summary>
        /// Add a entity into the entities container by reference.
        /// </summary>
        /// <param name="newEntity"></param>
        public void RegisterEntity(BaseGameEntity newEntity)
        {
            if (!EntityDic.ContainsKey(newEntity.ID))
            {
                EntityDic.Add(newEntity.ID, newEntity);
            }
        }

        /// <summary>
        /// Retrive a entity with given id.
        /// </summary>
        /// <param name="id">the id of the target entity.</param>
        /// <returns></returns>
        public BaseGameEntity GetEntityByID(int id)
        {
            if (EntityDic.ContainsKey(id))
            {
                return EntityDic[id];
            }

            return null;
        }

        /// <summary>
        /// Remove the entity from entities container by it's reference.
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(BaseGameEntity entity)
        {
            if (!System.Object.ReferenceEquals(entity, null))
            {
                if (EntityDic.ContainsKey(entity.ID))
                {
                    EntityDic.Remove(entity.ID);
                }
            }
        }

        /// <summary>
        /// Remove the entity with given id from entities container.
        /// </summary>
        /// <param name="id">The id of entity to remove.</param>
        public void RemoveEntityByID(int id)
        {
            if (EntityDic.ContainsKey(id))
            {
                EntityDic.Remove(id);
            }
        }

        /// <summary>
        /// Reset the entities container to empty.
        /// </summary>
        public void Reset()
        {
            EntityDic.Clear();
        }

        public void GetEntitiesByRaceSinal(int raceSignal, 
            List<BaseGameEntity> objList)
        {
            //DefenceSystem.Instance.Int2RaceType(raceSignal);

            //EntityDic.Values

            foreach (var item in EntityDic)
            { 
                if (item.Value.RaceSignal == raceSignal)
                {
                    objList.Add(item.Value);
                }
            }
        }

        public void GetEntitiesByRaceType(int raceType,
            List<BaseGameEntity> objList)
        {
            foreach (var item in EntityDic)
            {
                if (item.Value.RaceType == raceType)
                {
                    objList.Add(item.Value);
                }
            }
        }
    }
}
