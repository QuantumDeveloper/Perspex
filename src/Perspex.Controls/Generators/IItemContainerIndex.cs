// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System.Collections.Generic;

namespace Perspex.Controls.Generators
{
    public interface IItemContainerIndex : IItemContainers
    {
        void Add(IEnumerable<ItemContainer> containers);

        void InsertSpace(int index, int count);

        IEnumerable<ItemContainer> Remove(int index, int count);

        IEnumerable<ItemContainer> RemoveAndReindex(int index, int count);

        IEnumerable<ItemContainer> ClearRange(int index, int count);

        void Clear();
    }
}
