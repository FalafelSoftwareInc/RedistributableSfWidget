<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<%--These are the values we can use inside our template that come from our Tweet Model

    - ScreenName        | string
    - Author            | string
    - Message           | string
    - AccountUrl        | string
    - CreatedAt         | string
    - CreatedAtDateTime | DateTime (nullable)
    - ReplyUrl          | string
    - RetweetUrl        | string

    Use these variables as follows: 
    
    - <asp:Literal runat="server" Text='<%# Eval("VARIABLE_NAME") %>'></asp:Literal>
    - <%# Eval("VARIABLE_NAME") %>
    
--%>

<sf:ConditionalTemplateContainer ID="twitterTemplateContainer" runat="server">
    <Templates>
        <sf:ConditionalTemplate Left="TwitterWidgetRenderMode" Operator="Equal" Right="1" runat="server">
            <asp:Repeater ID="rptTweetsHome" runat="server">
                <HeaderTemplate>
                    <ul class="twitter homepage">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <div class="tweet">
                            <span class="title"><asp:Literal runat="server" ID="Message" Text='<%# DataBinder.Eval(Container.DataItem, "Message") %>'></asp:Literal></span>
                            <span class="date">Created at: <i><asp:Literal runat="server" ID="CreatedAtDateTime" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "CreatedAtDateTime")).ToShortDateString() %>'></asp:Literal></i> which is <i>
                                <asp:Literal runat="server" ID="CreatedAt" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedAt") %>'></asp:Literal> </i></span>
                            <span class="links">
                                <a href='<%# DataBinder.Eval(Container.DataItem, "RetweetUrl") %>'>
                                    <span class="icon retweet"></span><b>Retweet</b>
                                </a>                           
                                 <a href='<%# DataBinder.Eval(Container.DataItem, "ReplyUrl") %>'>
                                    <span class="icon reply"></span><b>Reply</b>
                                </a> 
                            </span>
                            </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </sf:ConditionalTemplate>
        <sf:ConditionalTemplate Left="TwitterWidgetRenderMode" Operator="Equal" Right="2" runat="server">
            <asp:Repeater ID="rptTweetsInterior" runat="server">
                <HeaderTemplate>
                    <ul class="twitter interior">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                          <div class="tweet">
                            <span class="title"><asp:Literal runat="server" ID="Message" Text='<%# DataBinder.Eval(Container.DataItem, "Message") %>'></asp:Literal></span>
                            <span class="date"><i><asp:Literal runat="server" ID="CreatedAtDateTime" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "CreatedAtDateTime")).ToShortDateString() %>'></asp:Literal></i> // <i>
                                <asp:Literal runat="server" ID="CreatedAt" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedAt") %>'></asp:Literal> </i></span>
                            <span class="links">
                                <a href='<%# DataBinder.Eval(Container.DataItem, "RetweetUrl") %>'>
                                    <span class="icon retweet"></span><b>Retweet</b>
                                </a>                           
                                 <a href='<%# DataBinder.Eval(Container.DataItem, "ReplyUrl") %>'>
                                    <span class="icon reply"></span><b>Reply</b>
                                </a> 
                            </span>
                            </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
                <SeparatorTemplate>
                    <span class="separator"></span>
                </SeparatorTemplate>
            </asp:Repeater>
        </sf:ConditionalTemplate>
    </Templates>
</sf:ConditionalTemplateContainer>