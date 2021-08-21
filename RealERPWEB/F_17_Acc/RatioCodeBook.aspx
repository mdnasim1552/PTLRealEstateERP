<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RatioCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RatioCodeBook" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <div class="container moduleItemWrpper">
        <script type="text/javascript">
            $(document).ready(function () {
                //For navigating using left and right arrow of the keyboard
                Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            });
            function pageLoaded() {
                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });
            };
        </script>
        
          
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px ">

                                            <asp:Label ID="LblBookName1" runat="server" CssClass="lblName lblTxt" Text="Code Book:"></asp:Label>

                                            <asp:DropDownList ID="ddlPRatioBook" runat="server" Width="250px" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:Label ID="lbalterofddl" runat="server" CssClass=" inputtextbox" Visible="False" Width="250px">></asp:Label>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>
                                         <div class="col-md-3 pading5px ">

                                            <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Size:"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="85px">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-success primaryBtn"></asp:Label>
                                        </div>
                                    </div>
                                   

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                            <asp:GridView ID="grvRatioCB" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" 
                                OnRowCancelingEdit="grvRatioCB_RowCancelingEdit" 
                                OnRowEditing="grvRatioCB_RowEditing" 
                                OnRowUpdating="grvRatioCB_RowUpdating" >

                                <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                                <Columns>


                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>

                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                    </asp:CommandField>

                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcode2"))+"-" %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                                MaxLength="4"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcode3")) %>'
                                                Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcode3")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                 
                                    <asp:TemplateField HeaderText="Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rdesc")) %>'
                                                Width="250px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                    </asp:TemplateField>  
                                    
                                    <asp:TemplateField HeaderText="Formula">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvformula" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rformula")) %>'
                                                Width="300px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rformula")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                    </asp:TemplateField>                                 

                                     <asp:TemplateField HeaderText="STD. Rate">
                                        <EditItemTemplate >
                                            <asp:TextBox ID="txtgvStdrate" runat="server" Font-Size="12px" MaxLength="100"  
                                                Style="border-top-style: none; border-right-style: none; text-align:right; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px" 
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdrate")).ToString("#,##0.00;(#,##0.00);") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="right" />
                                    </asp:TemplateField>

                                 <%--   <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcode")) %>'
                                                Visible="False"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                    </div>
                </div>
       </ContentTemplate>
    </asp:UpdatePanel>
  </div>
</asp:Content>

