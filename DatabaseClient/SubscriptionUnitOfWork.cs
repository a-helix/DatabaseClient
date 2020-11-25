using Repository;
using System.Collections.Generic;


namespace DatabaseClient
{
    public class SubscriptionUnitOfWork : IUnitOfWork<Subscription>
    { 
        protected IRepository<Subscription> _repositorie;
        protected Dictionary<string,Subscription> _readDict;
        protected List<Subscription> _createList;
        protected List<Subscription> _updateList;
        protected List<string> _deleteList;



        public SubscriptionUnitOfWork(IRepository<Subscription> client)
        {
            _repositorie = client;
            _readDict = new Dictionary<string, Subscription>();
            _createList = new List<Subscription>();
            _updateList = new List<Subscription>();
            _deleteList = new List<string>();
        }

        public void Commit()
        {
            foreach(var i in _createList)
            {
                _repositorie.Create(i);
            }
            _createList = new List<Subscription>();
            foreach (var i in _updateList)
            {
                _repositorie.Update(i);
                _readDict.Remove(i.ID);
            }
            _updateList = new List<Subscription>();
            foreach (var i in _deleteList)
            {
                _repositorie.Delete(i);
                _readDict.Remove(i);
            }
            _deleteList = new List<string>();
        }


        public void Add(Subscription coordinates)
        {
            _createList.Add(coordinates);
        }

        public Subscription GetByID(string unit)
        {
            Subscription feedback;
            if (!(_readDict.TryGetValue(unit, out feedback)))
            {
                feedback = _repositorie.Read(unit);
                _readDict.Add(unit, feedback);
            }
            return feedback;
        }

        public void Remove(string id)
        {
            _deleteList.Add(id);
        }

        public void Modify(Subscription unit)
        {
            _updateList.Add(unit);
        }
    }
}
