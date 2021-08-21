<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MktRentPaymentSchdule.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktRentPaymentSchdule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

        }
    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>


                                        <asp:Label ID="lblProjectmDesc" runat="server" Visible="False" Width="350px" CssClass="lblTxt lblName txtAlgLeft">

                                        </asp:Label>
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>

                                    </div>



                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblMaterial" runat="server" CssClass="lblTxt lblName txtAlgLeft"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="10"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="12">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>


                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>

                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="831px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowCancelingEdit="gvSpayment_RowCancelingEdit"
                            OnRowEditing="gvSpayment_RowEditing" OnRowUpdating="gvSpayment_RowUpdating">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnusize" runat="server" CommandArgument="lbtnusize"
                                            OnClick="lbtnusize_Click" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>


                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Min Booking Money">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minbam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Car Parking">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcparking" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cparking")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mgt Booking">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMgtBook" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:CommandField ShowEditButton="True" />

                                <asp:TemplateField HeaderText="Client Name">
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel2" runat="server">

                                            <div class="form-group">

                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:TextBox ID="txtSerachClient" runat="server" CssClass="inputTxt lblTxt inpPixedWidth" TabIndex="10"></asp:TextBox>

                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="ibtnSrchClient" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchClient_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                    </div>

                                                    <asp:DropDownList ID="ddlClientName" runat="server" CssClass="form-control inputTxt" Width="100" TabIndex="12">
                                                    </asp:DropDownList>

                                                </div>


                                            </div>


                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcclientname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewPersonal" runat="server">
                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="lperInfo" runat="server" CssClass="btn btn-success primaryBtn" Text="Personal Information"></asp:Label>
                                            <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>
                                            <asp:LinkButton ID="lbtnBack" runat="server"
                                                OnClick="lbtnBack_Click" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </fieldset>
                                <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="831px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px" ForeColor="Black" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Width="510px" Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                   Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>


                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <div class="form-group">
                                    <asp:Label ID="lperInfo0" runat="server" CssClass="btn btn-success primaryBtn" Text="Revenue Information"></asp:Label>
                                    <asp:Label ID="lblAcAmt" runat="server" Visible="False"></asp:Label>
                                    <div class="clearfix"></div>

                                </div>
                                <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="831px">
                                    <RowStyle Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvGcod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalCost" runat="server" Font-Bold="True" Font-Size="12px"
                                                  CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lFinalUpdateCost" runat="server" CssClass="btn btn-danger primarygrdBtn" OnClick="lFinalUpdateCost_Click"> Update </asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfrmdate" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdate")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px" Font-Size="11px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px" Font-Size="11px"></asp:TextBox>

                                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Duration">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvduration" runat="server" ForeColor="Black" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgUnitnum" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                                    Width="50px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Size">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvUSize" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate/Sft">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" ForeColor="Black" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rent/Month">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRentPMon" runat="server" ForeColor="Black" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rpmonth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvuamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Adv. Adjustment">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Height="18px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks"))%>'
                                                    Width="100px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                              <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="ldT" runat="server" CssClass="lblTxt lblName"
                                                Text="Discount (TK)"></asp:Label>
                                            <asp:Label ID="ldiscountt" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                            <asp:Label ID="ldp" runat="server" CssClass="lblTxt lblName"
                                                Text="Discount (%)"></asp:Label>
                                            <asp:Label ID="ldiscountp" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="form-group">
                                            <asp:Panel ID="Panel2" runat="server" Width="831px">

                                                <div class="form-group">
                                                    <div class="col-md-5 pading5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Sales Team</asp:Label>

                                                       
                                                            <asp:LinkButton ID="ibtnFindSalesteam" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindSalesteam_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                 

                                                         <asp:DropDownList ID="ddlSalesTeam" runat="server" CssClass="ddlPage margin5px" TabIndex="12" style="width:205px;">
                                                        </asp:DropDownList>
            </div>

            <div class="col-md-2 pull-right">
                                                        <asp:LinkButton ID="lbtnUpdateCAST" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdateCAST_Click">Update</asp:LinkButton>
                                                    </div>
                                                    <div class="clearfix"></div>

                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Aggrement Date</asp:Label>
                                                    <asp:TextBox ID="txtAggrementdate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtAggrementdate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtAggrementdate"></cc1:CalendarExtender>

                                                    <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Handover Date</asp:Label>
                                                    <asp:TextBox ID="txthandoverdate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txthandoverdate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txthandoverdate"></cc1:CalendarExtender>
                                                    <div class="clearfix"></div>
                                                </div>

                                            </asp:Panel>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Panel ID="Panel3" runat="server">

                                                <div class="form-group">
                                                    <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">First Ins. Date</asp:Label>
                                                    <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                                    <asp:Label ID="Label5" runat="server" Font-Size="11px" CssClass="lblTxt lblName">Total Installement</asp:Label>
                                                    <asp:TextBox ID="txtTInstall" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Duration</asp:Label>
                                                    <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="True"
                                                        CssClass="ddlPage" Width="120px">
                                                        <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                        <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                        <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                        <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                        <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                        <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                        <asp:ListItem Value="7">7  Month</asp:ListItem>
                                                        <asp:ListItem Value="8">8  Month</asp:ListItem>
                                                        <asp:ListItem Value="9">9  Month</asp:ListItem>
                                                        <asp:ListItem Value="10">10  Month</asp:ListItem>
                                                        <asp:ListItem Value="11">11  Month</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:LinkButton ID="lbtnGenerate" runat="server" OnClick="lbtnGenerate_Click" CssClass="btn btn-primary primaryBtn">Generate</asp:LinkButton>
                                                    <div class="clearfix"></div>
                                                </div>
                                                

                                            </asp:Panel>
                                            <asp:Panel ID="PanelAddIns" runat="server" Visible="False">
                                                    <div class="form-group">
                                                        <div class="col-md-3 pading5px asitCol3">
                                                            <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Installement</asp:Label>
                                                            <asp:TextBox ID="txtsrchInstallment" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>

                                                            <div class="colMdbtn">
                                                                <asp:LinkButton ID="ibtnFindInstallment" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindInstallment_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 pading5px asitCol3">
                                                            <asp:DropDownList ID="ddlInstallment" runat="server" CssClass="form-control inputTxt" TabIndex="12">
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:LinkButton ID="lbtnAddInstallment" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnAddInstallment_Click">Add</asp:LinkButton>
                                                        </div>
                                                        <div class="clearfix"></div>

                                                    </div>
                                                </asp:Panel>


                                            <asp:Panel ID="pnlSlab" runat="server" Visible="False">
                                                    <div class="form-group">
                                                        <div class="col-md-8 pading5px">
                                                            <asp:Label ID="lblfrmslab" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                                            <asp:TextBox ID="txtfrmslab" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                             <asp:Label ID="lbltoslab" runat="server" CssClass="lblTxt lblName">To</asp:Label>
                                                            <asp:TextBox ID="txttoslab" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                             <asp:Label ID="lblinsamt" runat="server" CssClass="lblTxt lblName">Installment </asp:Label>
                                                            <asp:TextBox ID="txtperslabamt" runat="server" CssClass="inputTxt inpPixedWidth" Style="text-align:right;"></asp:TextBox>
                                                            <asp:LinkButton ID="lbtnSlab" runat="server" CssClass="btn btn-primary primaryBtn margin5px" OnClick="lbtnSlab_Click">Put Data</asp:LinkButton>
                                                        </div>
                                                       
                                                       
                                                        <div class="clearfix"></div>

                                                    </div>
                                                </asp:Panel>

                                        </div>

                                        <div class="form-group">
                                            <asp:Panel ID="Panel5" runat="server">
                                                <div class="form-group">
                                                    <asp:Label ID="lPays" runat="server" CssClass="lblTxt lblName">Payment Shedule</asp:Label>
                                                    <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                                    <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True"
                                                        CssClass="chkBoxControl"
                                                        OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment" />
                                                    <asp:CheckBox ID="chkSegment" runat="server" AutoPostBack="True"
                                                        CssClass="chkBoxControl"
                                                        OnCheckedChanged="chkSegment_CheckedChanged" Text="Slab" />
                                                    
                                                     <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                                                        CssClass="chkBoxControl"
                                                        OnCheckedChanged="chkAddIns_CheckedChanged" Text="Add.Installment" />
                                                </div>


                                            </asp:Panel>
                                        </div>
                                    </div>
                                </fieldset>

                                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="223px" OnRowDeleting="gvPayment_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode3" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="450px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatpayment" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatpayment_Click">Update Payment Info</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" Font-Size="11px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lTotalPayment" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" OnClick="lTotalPayment_Click">Total Payment</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="100px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>



                                <asp:Panel ID="Panel4" runat="server">

                                    <fieldset class="scheduler-border fieldset_B">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-6 pading5px">
                                                    <asp:Label ID="lblAddWork" runat="server" CssClass="lblTxt lblName" Text="Additional Work"></asp:Label>


                                                    <asp:Label ID="lblValAddWork" runat="server" CssClass="lblTxt lblName txtAlgLeft">

                                                    </asp:Label>
                                                    <asp:Label ID="lmsg111" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>

                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6 pading5px">
                                                    <asp:Label ID="lblDedWork" runat="server" CssClass="lblTxt lblName" Text="Deduction Work"></asp:Label>


                                                    <asp:Label ID="lblValDedWork" runat="server" CssClass="lblTxt lblName txtAlgLeft">

                                                    </asp:Label>

                                                </div>
                                                <div class="clearfix"></div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6 pading5px">
                                                    <asp:Label ID="lblNetTotalPayment" Font-Size="11px" runat="server" CssClass="lblTxt lblName" Text="Net Total Payment"></asp:Label>


                                                    <asp:Label ID="lblValNetTotalPayment" runat="server" CssClass="lblTxt lblName txtAlgLeft">

                                                    </asp:Label>

                                                </div>
                                                <div class="clearfix"></div>

                                            </div>
                                        </div>
                                    </fieldset>


                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="VLoanInfo" runat="server">

                                <div class="row">
                                    <asp:LinkButton ID="lbtnBackCost" CssClass="btn btn-danger primaryBtn" runat="server" OnClick="lbtnBack_Click">Back</asp:LinkButton>
                                    <asp:GridView ID="gvLoanInformation" CssClass=" table-striped table-hover table-bordered grvContentarea" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="831px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCodeLoan" runat="server" ForeColor="Black" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc3" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgvalloan" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateLoanInfo" runat="server" Font-Bold="True"
                                                        Font-Size="12px" ForeColor="#000" OnClick="lUpdateLoanInfo_Click"
                                                        Style="text-decaration: none;">Update Loan Info</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvValloan" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px"
                                                        Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="510px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewResStatus" runat="server">
                                <asp:LinkButton ID="lbtnBackResStatus" runat="server" OnClick="lbtnBack_Click" CssClass="btn btn-danger primaryBtn">Back</asp:LinkButton>
                                <div class="row">
                                    <asp:GridView ID="gvRegStatus" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="831px"
                                        Style="margin-right: 0px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCodeReg" runat="server" ForeColor="Black"
                                                        Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDescReg" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgvalReg" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateRegis" CssClass="btn btn-danger primaryBtn" runat="server" OnClick="lUpdateRegis_Click">Update Registration</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderTemplate>
                                                    <table style="width: 13%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLgDept" runat="server"
                                                                    Style="text-align: center" Text="Received from Legal" Width="106px"
                                                                    Height="16px"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLgDept0" runat="server" Height="16px"
                                                                    Style="text-align: center" Text="Status &amp; Date" Width="106px"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvValRecleg" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="200px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="">

                                                <HeaderTemplate>
                                                    <table style="width: 13%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLgDept" runat="server"
                                                                    Style="text-align: center" Text="Provided to Client" Width="106px"
                                                                    Height="16px"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLgDept0" runat="server" Height="16px"
                                                                    Style="text-align: center" Text="Status &amp; Date" Width="106px"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvValprotoclient" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc2")) %>'
                                                        Width="200px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>








