using Repository;
using System.Collections.Generic;


namespace DatabaseClient
{
    public class SubscriptionUnitOfWork : IRepository<Subscription>, IUnitOfWork<Subscription>
    { 
        private IRepository<Subscription> _repositorie;
        private Dictionary<string,Subscription> _readList;
        private List<Subscription> _createList;
        private List<Subscription> _updateList;
        private List<string> _deleteList;



        public SubscriptionUnitOfWork(IRepository<Subscription> client)
        {
            _repositorie = client;
            _readList = new Dictionary<string, Subscription>();
            _createList = new List<Subscription>();
            _updateList = new List<Subscription>();
            _deleteList = new List<string>();
        }

        public void Save()
        {
            foreach(var i in _createList)
            {
                _repositorie.Create(i);
            }
            _createList = new List<Subscription>();
            foreach (var i in _updateList)
            {
                _repositorie.Update(i);
                _readList.Remove(i.ID);
            }
            _updateList = new List<Subscription>();
            foreach (var i in _deleteList)
            {
                _repositorie.Delete(i);
                _readList.Remove(i);
            }
            _deleteList = new List<string>();
        }


        public void Create(Subscription coordinates)
        {
            _createList.Add(coordinates);
        }

        public Subscription Read(string unit)
        {
            if (_readList.ContainsKey(unit))
                return _readList[unit];
            Subscription feedback = _repositorie.Read(unit);
            _readList.Add(unit, feedback);
            return feedback;
        }

        public void Delete(string id)
        {
            _deleteList.Add(id);
        }

        public void Update(Subscription unit)
        {
            _updateList.Add(unit);
        }
    }
}
