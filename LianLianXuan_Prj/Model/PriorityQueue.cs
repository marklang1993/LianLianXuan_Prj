using System.Collections.Generic;

namespace LianLianXuan_Prj.Model
{
    public class PriorityQueue
    {
        private readonly SortedList<int, Queue<Position>> _priorityList;

        public PriorityQueue()
        {
            _priorityList = new SortedList<int, Queue<Position>>();
        }

        public void Enqueue(int priority, Position pos)
        {
            Queue<Position> queue;
            if (_priorityList.TryGetValue(priority, out queue))
            {
                // Current priority exists
                queue.Enqueue(pos);
            }
            else
            {
                // Current priority does not exist
                queue = new Queue<Position>();
                queue.Enqueue(pos);
                _priorityList.Add(priority, queue);
            }

        }

        public Position Dequeue()
        {
            if (!IsEmpty())
            {
                // Element exists
                IEnumerator<KeyValuePair<int, Queue<Position>>> enumerator = _priorityList.GetEnumerator();
                enumerator.MoveNext();
                int priority = enumerator.Current.Key;
                Queue<Position> queue = enumerator.Current.Value;

                Position pos = queue.Dequeue();
                // Check the current queue is empty
                if (queue.Count == 0)
                {
                    _priorityList.Remove(priority);
                }
                return pos;
            }
            // Element does not exist
            return null;
        }

        public bool IsEmpty()
        {
            return _priorityList.Count == 0;
        }
    }
}
