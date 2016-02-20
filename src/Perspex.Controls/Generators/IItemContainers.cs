﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;

namespace Perspex.Controls.Generators
{
    public interface IItemContainers
    {
        event EventHandler<ItemContainerEventArgs> Added;

        event EventHandler<ItemContainerEventArgs> Removed;

        bool IsIndexed { get; }

        IControl FromIndex(int index);

        IControl FromItem(object item);

        int IndexOf(IControl container);
    }
}