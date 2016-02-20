// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Perspex.Controls.Generators
{
    public class ItemContainerIndex : IItemContainerIndex
    {
        private List<ItemContainer> _containers = new List<ItemContainer>();

        public event EventHandler<ItemContainerEventArgs> Added;

        public event EventHandler<ItemContainerEventArgs> Removed;

        public bool IsIndexed => true;

        public void Add(IEnumerable<ItemContainer> containers)
        {            
            var list = new List<ItemContainer>();
            int startingIndex = -1;

            foreach (var c in containers)
            {
                // NASTY HACK HERE
                if (startingIndex == -1)
                {
                    startingIndex = c.Index;
                }

                while (_containers.Count < c.Index)
                {
                    _containers.Add(null);
                }

                if (_containers.Count == c.Index)
                {
                    _containers.Add(c);
                }
                else if (_containers[c.Index] == null)
                {
                    _containers[c.Index] = c;
                }
                else
                {
                    throw new InvalidOperationException("Container already created.");
                }

                list.Add(c);
            }

            if (startingIndex != -1)
            {
                Added?.Invoke(this, new ItemContainerEventArgs(startingIndex, list));
            }
        }

        public void InsertSpace(int index, int count)
        {
            _containers.InsertRange(index, Enumerable.Repeat<ItemContainer>(null, count));
        }

        public IEnumerable<ItemContainer> Remove(int index, int count)
        {
            var result = new List<ItemContainer>();

            for (int i = index; i < index + count; ++i)
            {
                if (i < _containers.Count)
                {
                    result.Add(_containers[i]);
                    _containers[i] = null;
                }
            }

            Removed?.Invoke(this, new ItemContainerEventArgs(index, result));

            return result;
        }

        public IEnumerable<ItemContainer> RemoveAndReindex(int index, int count)
        {
            var result = _containers.GetRange(index, count);
            _containers.RemoveRange(index, count);
            Removed?.Invoke(this, new ItemContainerEventArgs(index, result));
            return result;
        }

        public IEnumerable<ItemContainer> ClearRange(int index, int count)
        {
            var result = new List<ItemContainer>(count);
            
            for (int i = index; i < index + count; ++i)
            {
                result.Add(_containers[i]);
                _containers[i] = null;
            }

            Removed?.Invoke(this, new ItemContainerEventArgs(index, result));

            return result;
        }

        public void Clear()
        {
            var old = _containers.ToList();
            _containers.Clear();
            Removed?.Invoke(this, new ItemContainerEventArgs(0, old));
        }

        public IControl FromIndex(int index)
        {
            if (index < _containers.Count)
            {
                return _containers[index]?.ContainerControl;
            }

            return null;
        }

        public IControl FromItem(object item)
        {
            return _containers.FirstOrDefault(x => x.Item == item)?.ContainerControl;
        }

        public int IndexOf(IControl container)
        {
            var index = 0;

            foreach (var i in _containers)
            {
                if (i?.ContainerControl == container)
                {
                    return index;
                }

                ++index;
            }

            return -1;
        }
    }
}
