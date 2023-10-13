using System;

namespace Frame.UI
{
    public interface IUI_Panel
    {
        public void Init(Action action);
    }

    public interface IUI_Ctrl
    {
        public void EventRegister();
    }

}

