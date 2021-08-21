<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpKpiEntry04Det.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.EmpKpiEntry04Det" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-align: right;
            font-weight: bold;
        }

        .auto-style2 {
            font-weight: bold;
        }

        .auto-style3 {
            text-align: left;
            width: 82px;
        }

        .last_bold {
            font-size: 14px !important;
            color: green !important;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            var gridview = $('#<%=this.gvEmpWlistEntry.ClientID %>');
            gridview.Scrollable();
        };

    </script>
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">


                                <div class="form-group">
                                    <div class="col-md-5 pading5px ">
                                        <asp:CheckBox ID="chkcopy" runat="server"  TabIndex="10" Text="Copy " CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="chkcopy_CheckedChanged" />

                                    </div>


                                    <div class="col-md-2 pading5px asitCol3">
                                    </div>
                                </div>
                                <asp:Panel ID="pnlCopy" runat="server" Visible="false" Style="border: 1px solid blue;">

                                    <div class="form-group">
                                        <div class="col-md-5 pading5px ">
                                            <asp:Label ID="lblDate" runat="server" CssClass=" smLbl_to" Text="Date"></asp:Label>

                                            <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtdate">
                                        </cc1:CalendarExtender>



                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnCopy" runat="server" Text="Copy" OnClick="lbtnCopy_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>

                            </div>
                        </fieldset>

                            <asp:GridView ID="gvEmpWlistEntry" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="668px" CssClass="table-striped table-hover table-bordered grvContentarea " OnRowDataBound="gvEmpWlistEntry_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvActcode" runat="server"
                                                ForeColor="Black" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work List">

                                        <ItemTemplate>
                                            <asp:Label ID="lblGvCode" runat="server"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="500px">
                                            </asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="target work">

                                        <ItemTemplate>
                                            <asp:Label ID="lblGvtarget" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wqty")).ToString("#,##0.00;-#,##0.00; ")%>'
                                                Width="60px" Style="text-align: right"></asp:Label>


                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="%">


                                        <ItemTemplate>

                                            <asp:Label ID="lblppercent" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppercent"))==0.00)?Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppercent")).ToString("#,##0.00;(#,##0.00); "):Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppercent")).ToString("#,##0.00;(#,##0.00); ")+" %"%>'
                                                Width="60px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="execution</Br> Work">


                                        <ItemTemplate>

                                            <asp:TextBox ID="txtQtyWork" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acqty")).ToString("#,##0;-#,##0; ")%>'
                                                Width="70px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFwqty" runat="server"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Note">


                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgbnote" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "note")) %>'
                                                Width="200px" Style="text-align: left"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn primaryBtn" Visible="false"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Client">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                Width="150px" Style="text-align: left"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle />
                                    </asp:TemplateField>

                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>
                  
               
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-12 formBtn ">

                                            <div class="pull-right">
                                                <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" OnClick="lnkbtnSave_Click"><span class="flaticon-add118"></span> Save</asp:LinkButton>
                                                <%--<asp:HyperLink ID="lnkbtnEdit" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" NavigateUrl="#"><span class="flaticon-edit26"></span> Edit</asp:HyperLink>--%>


                                                <asp:HyperLink ID="lnkbtnClose" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" NavigateUrl="~/F_39_MyPage/EmpKpiEntry04.aspx?Type=Mgt"><span class="flaticon-delete47 text-danger "></span>Close</asp:HyperLink>

                                                <%-- <asp:HyperLink ID="lnkbtnAdd" runat="server" CssClass="btn  btn-primary primaryBtn"Style="margin: 0 5px;"  NavigateUrl="~/F_17_Acc/AccInv.aspx">Add</asp:HyperLink>--%>
                                            </div>
                                        </div>





                                    </div>



                                </div>
                            </fieldset>
                     


                </div>
            </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

