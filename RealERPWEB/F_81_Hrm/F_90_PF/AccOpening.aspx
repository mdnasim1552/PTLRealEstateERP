<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccOpening.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.AccOpening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div#ContentPlaceHolder1_ddlCompany_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlProjectName_chzn {
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
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
            <div class="card mt-5">
                <div class="contentPart">

                    <div class="card-header">

                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <div class="col-lg-6 ">
                                        <asp:Label ID="lblopndate" runat="server" CssClass="lblTxt lblName">Opening Date</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-lg-6 ">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>



                            </div>
                            <div class="col-lg-3">

                                <asp:Label ID="lblacccode1" runat="server" CssClass="lblTxt lblName">Accounts Code

                                <asp:LinkButton ID="ImageButton2" runat="server"   OnClick="ImageButton2_Click"><i class="fa fa-search"> </i></asp:LinkButton>

                                </asp:Label>
                                <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control"></asp:TextBox>



                            </div>
                            <div class="col-lg-2" id="mainDDlPage" runat="server">
                                 <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                <asp:DropDownList ID="dgv2ddlPageNo" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="dgv2ddlPageNo_SelectedIndexChanged">
                                   
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>1300</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-lg-2">

                                <asp:LinkButton ID="ImageButton1" runat="server" CssClass="btn btn-primary srearchBtn mt20" Text="OK" OnClick="ImageButton1_Click"></asp:LinkButton>


                            </div>
                        </div>



                    </div>
                </div>
                <div class="body">
                    <div class="row">

                        <asp:GridView ID="dgv2" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" OnRowCreated="dgv2_RowCreated"
                            PagerSettings-Position="Bottom" PagerStyle-BackColor="#4A89BC"
                            PagerSettings-Visible="true"
                            PagerStyle-HorizontalAlign="Center" RowStyle-Font-Size="12px" ShowFooter="True"
                            OnRowCommand="dgv2_RowCommand" PageSize="50" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ActCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <FooterTemplate>


                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server"
                                            OnClick="lnkFinalUpdate_Click"
                                            CssClass="btn btn-danger primarygrdBtn">Final Update</asp:LinkButton>
                                         
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Font-Size="11px" Width="400px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderText="Level"
                                    ItemStyle-HorizontalAlign="Center">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="LnkfTotal" runat="server" OnClick="LnkfTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total :</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvlnkLevel" runat="server" OnClick="gvlnkLevel_Click"
                                            onmouseover="style.color='#FF9999'" onmouseout="style.color='blue'"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                            Width="50px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Width="103px" Font-Bold="True" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="103px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Width="103px" ReadOnly="True" Font-Bold="True" Style="text-align: right"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="103px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                    </div>



                    <asp:Panel ID="pnlsub" runat="server">
                        <div class="card-header">
                            <h6 class="m-0">Resource Entry Screen</h6>
                        </div>
                        <div class="row">



                            <div class="col-md-3">
                                <asp:Label ID="lblacccode" runat="server">Accounts Code</asp:Label>
                                <asp:TextBox ID="txtActcode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label11" runat="server">Resource Code</asp:Label>
                                <asp:TextBox ID="txtResSearch" runat="server" CssClass="form-control"></asp:TextBox>


                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblPage" runat="server" Text="Page"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>1300</asp:ListItem>
                                </asp:DropDownList>


                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-2 pull-right">
                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-info btn-sm mt-4" OnClick="lnkSubmit_Click">Home</asp:LinkButton>

                            </div>
                             
                        </div>
                        <div class="row mt-2">

                            <asp:GridView ID="dgv3" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnPageIndexChanging="dgv3_PageIndexChanging"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblrescode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="gvlblResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="250px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Card #"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcardno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                Width="70px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnUpdateRes" runat="server" Font-Bold="True" CssClass="btn btn-success btn-sm "
                                                OnClick="lnkbtnUpdateRes_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="gvlnkFTotal" runat="server" CssClass="btn btn-primary btn-sm " OnClick="gvlnkFTotal_Click">Total 
                                            </asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr. Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent"
                                                Font-Bold="True"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Width="116px" ReadOnly="True" Style="text-align: right">></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr. Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Width="106px" Style="text-align: right">></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Company Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxcompamt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "compamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="gvtxtftcompamt" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Width="106px" Style="text-align: right">0.00</asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>




                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>

                        </div>


                    </asp:Panel>





                    <!-- End of contentpart-->
                </div>
                <!-- End of Container-->
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


