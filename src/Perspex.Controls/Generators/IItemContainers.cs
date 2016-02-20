// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace Perspex.Controls.Generators
{
    public interface IItemContainers
    {
        bool IsIndexed { get; }

        IControl FromIndex(int index);

        IControl FromItem(object item);

        int IndexOf(IControl container);
    }
}
