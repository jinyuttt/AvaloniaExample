
//using System.Collections.Generic;
//using System.Drawing;

//namespace ChatRenderDemo.Linux.CustomizeRender
//{
//    //自定义
//    internal class ChatItemCustomize : BaseChatItem
//    {
//        private static string FontName = "微软雅黑";

//        private Size customizeRenderSize = new Size(222, 102);
//        private string title = "邀请您使用签到";
//        private string signinState = "未签到";
//        private string title_Limit = null;
//        private string signinState_Limit = null;
//        private int fontSize = 14;
//        private int signin_FontSize = 12;
//        private static Color borderColor = Color.FromArgb(135, 206, 235);
//        private static Color panelColor = Color.FromArgb(168, 213, 254);
//        private static Color gridColor = Color.White;
//        private static Color seperatorLineColor = Color.FromArgb(243, 241, 242);

//        private static Color panelSelectedColor = Color.FromArgb(168, 213, 254);
//        private static Color gridSelectedColor = Color.FromArgb(226, 236, 247);
//        private static Color seperatorLineSelectedColor = Color.FromArgb(200, 201, 204);

//        private static Color textColor = Color.FromArgb(105, 105, 105);
//        private Color ellipseColor = Color.Red;
//        private static Color signinEllipseColor = Color.FromArgb(9, 241, 117);

//        private List<LinkLabel> linkLabelList = new List<LinkLabel>();
//        private LinkLabel linkLabel_Signin;

//        public override ChatMessageType ChatMessageType { get { return ChatMessageType.Customized; } }

//        public ChatItemCustomize(IRender render, IRenderDataProvider provider, int renderWidth, string msgID, string userID, bool me, bool drawName) : base(render, provider, renderWidth, msgID, userID, me, drawName)
//        {

//        }

//        protected override Size ReComputeMainBodyOnRenderSurfaceSizeChanged()
//        {
//            return this.customizeRenderSize;
//        }

//        private void Label_Clicked(LinkLabel label)
//        {
//            this.ellipseColor = signinEllipseColor;
//            this.linkLabel_Signin.Visiable = false;
//            this.signinState = "签到";
//            this.signinState_Limit = ChatRenderHelper.LimitText(base.basicRender, this.signinState, fontSize, 150);
//            base.SpringNeedRedraw();
//        }

//        private void Label_NeedRedraw(LinkLabel label)
//        {
//            base.SpringNeedRedraw();
//        }

//        protected override void OnMainBodyChanged(bool initialized)
//        {
//            Size size = base.basicRender.ComputeStringRenderSize(title, RenderSettings.TextFontSize);

//            if (this.linkLabelList.Count == 0 && size.Width > 0)
//            {
//                //确保要显示的文字不会超过最大宽度  
//                this.title_Limit = ChatRenderHelper.LimitText(base.basicRender, this.title, fontSize, 150);
//                this.signinState_Limit = ChatRenderHelper.LimitText(base.basicRender, this.signinState, fontSize, 150);

//                this.linkLabel_Signin = new LinkLabel(base.basicRender, "点击签到", null, signin_FontSize);
//                this.linkLabel_Signin.Visiable = true;
//                this.linkLabelList.Add(this.linkLabel_Signin);

//                foreach (LinkLabel label in this.linkLabelList)
//                {
//                    label.NeedRedraw += Label_NeedRedraw;
//                    label.Clicked += Label_Clicked;
//                }
//            }

//            if (this.linkLabelList.Count > 0)
//            {
//                this.linkLabel_Signin.Position = new Point(base.MainBody.X + base.MainBody.Width - 80, base.MainBody.Y + 62);
//            }
//        }


//        public override void Render(ISysRender sysRender, int startY)
//        {
//            //绘制头像、名称
//            base.Render(sysRender, startY);

//            Point backgroundPosition = base.ContentBackgroundPosition;
//            if (base.IsMe)
//            {
//                backgroundPosition = new Point(base.ContentBackgroundPosition.X + base.MainBodyMaxWidth - this.customizeRenderSize.Width, base.ContentBackgroundPosition.Y);
//            }

//            Point start = new Point(backgroundPosition.X, backgroundPosition.Y + startY);
//            //绘制背景色
//            sysRender.FillRectangle(borderColor, new Rectangle(start.X, start.Y, customizeRenderSize.Width, customizeRenderSize.Height));
//            //浅蓝色矩形
//            sysRender.FillRectangle(this.isMouseIn ? panelSelectedColor : panelColor, new Rectangle(start.X + 2, start.Y + 2, customizeRenderSize.Width - 4, customizeRenderSize.Height - 4));
//            //白色矩形的边框
//            sysRender.FillRectangle(borderColor, new Rectangle(start.X + 9, start.Y + 9, customizeRenderSize.Width - 18, customizeRenderSize.Height - 18));
//            //白色矩形
//            sysRender.FillRectangle(this.isMouseIn ? gridSelectedColor : gridColor, new Rectangle(start.X + 10, start.Y + 10, customizeRenderSize.Width - 20, customizeRenderSize.Height - 20));
//            //分割线
//            sysRender.FillRectangle(this.isMouseIn ? seperatorLineSelectedColor : seperatorLineColor, new Rectangle(start.X + 36, start.Y + 51, customizeRenderSize.Width - 72, 1));
//            //"签到"
//            sysRender.DrawString(textColor, new Point(start.X + 30, start.Y + 22), this.title_Limit, fontSize, 0, ChatItemCustomize.FontName);
//            //红色圆圈
//            sysRender.FillEllipse(ellipseColor, new Point(start.X + 35, start.Y + 70), 5, 5);
//            //"未签到" 
//            sysRender.DrawString(textColor, new Point(start.X + 50, start.Y + 60), this.signinState_Limit, fontSize, 0, ChatItemCustomize.FontName);
//            foreach (LinkLabel label in this.linkLabelList)
//            {
//                label.Render(sysRender, startY);
//            }
//        }

//        public override void OnMouseLeave()
//        {
//            if (this.isMouseIn)
//            {
//                this.isMouseIn = false;
//                base.SpringNeedRedraw();
//            }
//        }

//        private bool isMouseIn = false;
//        public override void OnMouseMove(int startY, Point p)
//        {
//            if (!this.isMouseIn)
//            {
//                this.isMouseIn = true;
//                base.SpringNeedRedraw();
//            }
//        }

//        public override bool OnMouseDown(bool leftButton, int startY, Point p)
//        {
//            Point pt2 = new Point(p.X, p.Y - startY);
//            foreach (LinkLabel label in this.linkLabelList)
//            {
//                if (label.Visiable && label.Contains(pt2))
//                {
//                    label.MouseClick();
//                    return true;
//                }
//            }
//            return false;
//        }
//    }
//}
