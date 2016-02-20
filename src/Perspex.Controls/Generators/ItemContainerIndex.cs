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

        public bool IsIndexed => true;

        public void Add(IEnumerable<ItemContainer> containers)
        {
            foreach (var c in containers)
            {
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

            return result;
        }

        public IEnumerable<ItemContainer> RemoveAndReindex(int index, int count)
        {
            var result = _containers.GetRange(index, count);
            _containers.RemoveRange(index, count);
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

            return result;
        }

        public void Clear()
        {
            _containers.Clear();
        }

        public ItemContainer FromIndex(int index)
        {
            if (index < _containers.Count)
            {
                return _containers[index];
            }

            return null;
        }

        public ItemContainer FromItem(object item)
        {
            return _containers.FirstOrDefault(x => x.Item == item);
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
