<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BgdLevelRate.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.BgdLevelRate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
      


    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });



        function pageLoaded() {

            try {
                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);

                });


                var gvConLevel = $('#<%=this.gvConLevel.ClientID%>');
                gvConLevel.Scrollable();

                $('.chzn-select').chosen({ search_contains: true });









            }


            catch (e) {

                alert(e);
            }


        }



    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lbltxtprojectname" runat="server" CssClass="control-label"> Project Name</asp:Label>
                                <asp:LinkButton ID="ImgbtnFindImpNo" CssClass="srearchBtn" runat="server" OnClick="ImgbtnFindImpNo_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                                <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" TabIndex="3">
                                </asp:DropDownList>
                                <asp:Label ID="lblProjectName" runat="server" Visible="false" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lgvPage" runat="server" CssClass="control-label" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="500">500</asp:ListItem>
                                    <asp:ListItem Value="800">800</asp:ListItem>
                                    <asp:ListItem Value="1000">1000</asp:ListItem>
                                    <asp:ListItem Value="1200">1200</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 20px;">
                            <asp:CheckBox ID="chkUllock" runat="server" Text="Unlock" AutoPostBack="True" Visible="False" CssClass="btn btn-sm btn-primary primaryBtn chkBoxControl" OnCheckedChanged="chkUllock_CheckedChanged" />
                        </div>

                    </div>
                    
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvSubRate" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvSubRate_PageIndexChanging" ShowFooter="True"
                                    Style="text-align: left" Width="600px">
                                    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Labour">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlabdesc" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "flrdes").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="300px">



                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlabunit" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub.Con.Rate">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <%--<FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkbtnUpdate_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSRate" runat="server" BorderStyle="None"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "subconrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Work. Rate">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvwrkrate" runat="server" BorderStyle="None"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "workrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="row" id="lbldet" runat="server" visible="false">
                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="Floor"></asp:Label>
                                            <asp:DropDownList ID="ddlFloor" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" AutoPostBack="True">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="Catagory"></asp:Label>
                                            <asp:DropDownList ID="ddlCat" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" AutoPostBack="True">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 22px;">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-sm btn-primary primaryBtn" OnClick="lbtnSearch_Click"> <i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>

                                <asp:GridView ID="gvConLevel" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvConLevel_PageIndexChanging" ShowFooter="True"
                                    Style="text-align: left" Width="744px">
                                    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFlrDes0" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Item Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItemDes0" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptUnit2" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bgd.Qty">
                                            <%--<FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnUpdatelevel" runat="server"
                                                    CssClass="btn btn-danger primaryBtn" OnClick="lnkbtnUpdatelevel_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlQty" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkItem" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "markitem"))=="True" %>'
                                                    Width="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="ViewItemLock" runat="server">
                                <asp:GridView ID="gvItemlk" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvItemlk_PageIndexChanging" ShowFooter="True"
                                    Style="text-align: left" Width="744px">
                                    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoItemlk" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFlrDesItemlk" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Item Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItemDesItemlk" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptUnitItemlk" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bgd.Qty">
                                            <%--<FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnUpdateItemlk" runat="server"
                                                    CssClass="btn btn-danger primaryBtn" OnClick="lnkbtnUpdateItemlk_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlQty" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllfrmItemlk" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkAllfrmItemlk_CheckedChanged" Text="ALL " />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkItemlk" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemlock"))=="True" %>'
                                                    Width="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="Viewitemlock02" runat="server">

                                <asp:GridView ID="gvitemlk02" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvitemlk02_PageIndexChanging" ShowFooter="True"
                                    Style="text-align: left; margin-right: 0px;" Width="572px">
                                    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoItemlk02" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Description">

                                            <%--   <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnUpdateitemlk02" runat="server"
                                                    CssClass="btn btn-danger primaryBtn" OnClick="lnkbtnUpdateitemlk02_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>--%>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItemDesItemlk02" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptUnitItemlk02" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllfrmItemlk02" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkAllfrmItemlk02_CheckedChanged" Text="ALL " />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkItemlk" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemlock"))=="True" %>'
                                                    Width="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                   
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

