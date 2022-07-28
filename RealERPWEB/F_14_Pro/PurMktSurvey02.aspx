<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurMktSurvey02.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurMktSurvey02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });
            var gridview = $('#<%=this.gvMSRInfo2.ClientID %>');
            $.keynavigation(gridview);
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label11" runat="server" CssClass=" lblName lblTxt" Text="Survey No.:"></asp:Label>

                                            <asp:Label ID="lblCurMSRNo1" runat="server" CssClass="inputtextbox" Text="MSR00-"></asp:Label>

                                            <asp:TextBox ID="txtCurMSRNo2" runat="server" CssClass="inputtextbox">00000</asp:TextBox>

                                            <asp:Label ID="Label13" runat="server" CssClass=" smLbl_to" Text="Date:"></asp:Label>

                                            <asp:TextBox ID="txtCurMSRDate" runat="server" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurMSRDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>

                                            <asp:LinkButton ID="lbtnMSROk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnMSROk_Click">Ok</asp:LinkButton>

                                            <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lblPreMrList" runat="server" CssClass=" lblName lblTxt" Text="Previous MSR:"></asp:Label>

                                            <asp:TextBox ID="txtPreMSRSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindPreMR" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindPreMR_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlPrevMSRList" runat="server" Width="322px" CssClass="ddlPage "></asp:DropDownList>

                                        </div>
                                    </div>

                                </asp:Panel>
                                <asp:Panel ID="pnlSupMat" runat="server" Visible="False">
                                    <div class="form-group">
                                        <div class="col-md-3  pading5px asitCol3">

                                            <asp:Label ID="Label10" runat="server" CssClass=" lblName lblTxt" Text="Supplier List:"></asp:Label>

                                            <asp:TextBox ID="txtMSRSupSearch" runat="server" CssClass="inputtextbox" Style="width: 63px;"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindSup" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindSup_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>

                                        <div class="col-md-4 pading5px asitCol4 ">
                                            <asp:DropDownList ID="ddlMSRSupl" runat="server" AutoPostBack="True" Width="322px" CssClass="ddlPage chzn-select"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lbtnMSRSup" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnMSRSup_Click">Select Supplier</asp:LinkButton>
                                        </div>




                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3  pading5px asitCol3 asitCol3">

                                            <asp:Label ID="Label9" runat="server" CssClass=" lblName lblTxt" Text="Materials List:"></asp:Label>

                                            <asp:TextBox ID="txtMSRResSearch" runat="server" CssClass="inputtextbox" Style="width: 63px;"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindMat" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindMat_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                        </div>

                                        <div class="col-md-4 pading5px asitCol4 ">
                                            <asp:DropDownList ID="ddlMSRRes" runat="server" AutoPostBack="True" Width="322px" OnSelectedIndexChanged="ddlMSRRes_SelectedIndexChanged" CssClass="ddlPage chzn-select"></asp:DropDownList>

                                        </div>

                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lbtnMSRSelect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnMSRSelect_Click">Select Materials</asp:LinkButton>

                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblspecificationms" runat="server" CssClass="lblTxt lblName" Text="Specification"></asp:Label>
                                            <asp:TextBox ID="txtsrchSpecification3" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindSpecificationms" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSpecificationms_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-4 pading5px  asitCol4">
                                            <asp:DropDownList ID="ddlSpecificationms" runat="server" CssClass=" chzn-select  form-control inputTxt" TabIndex="6">
                                            </asp:DropDownList>

                                        </div>



                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>

                        <div class="table table-responsive">

                            <asp:GridView ID="gvMSRInfo2" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True"
                                OnRowDataBound="gvMSRInfo2_RowDataBound" OnRowCreated="gvMSRInfo2_RowCreated">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnMrsnodelete" runat="server" OnClick="lbtnMrsnodelete_Click"><span class="glyphicon glyphicon-remove"> </span></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="rsircode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrsircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="spcfcode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvspcfcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Materials Description ">

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary  primarygrdBtn">Total</asp:LinkButton>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRResDesc" runat="server"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                Width="150px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Requirement">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvMSRqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnMSRUpdate" runat="server" OnClick="lbtnMSRUpdate_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate1" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">


                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt1" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>





                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate2" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt2" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount2" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate3" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt3" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount3" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate4" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>   
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt4" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount4" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterStyle HorizontalAlign="RIght" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate5" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>   
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt5" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount5" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterStyle HorizontalAlign="RIght" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Previous App.rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaprovrate" runat="server"
                                                Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovrate"))=="0.00")?"" :Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovrate"))  %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvMSRRemarks" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "msrrmrk").ToString() %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>





                            <asp:GridView ID="gvterm" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSupplierdelete" runat="server" OnClick="lbtnSupplierdelete_Click"  OnClientClick="return confirm('Are you Sure to Delete This Suppliers ?');"><span class="glyphicon glyphicon-remove" > </span></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Suppliercode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvssircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Supplier">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDiscount" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Carring Charge">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvccharge" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ccharge").ToString() %>'
                                                Width="120px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payment Term">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvpayterm" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "payterm").ToString() %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Quotation Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCurQuTDate" runat="server" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"
                                                Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"qutdate")).Year==1900?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"qutdate")).ToString("dd-MMM-yyyy")) %>'> </asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurQuTDate_CalendarExtender" runat="server"
                                                TargetControlID="txtCurQuTDate"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Working Time">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtworkline" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "worktime").ToString() %>'
                                                Style="text-align: left; background-color: Transparent"
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Notes">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNotes" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "notes").ToString() %>'
                                                Style="text-align: left; background-color: Transparent"
                                                Width="180px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Good WIll">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgoodwill" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "goodwill").ToString() %>'
                                                Style="text-align: left; background-color: Transparent"
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Material Availability">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtmatavailable" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "matavailable").ToString() %>'
                                                Style="text-align: left; background-color: Transparent"
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delivery Condition">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtdelcon" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "delcon").ToString() %>'
                                                Style="text-align: left; background-color: Transparent"
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AIT">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtait" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ait").ToString() %>'
                                                Style="text-align: left; background-color: Transparent"
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>


                            <asp:Panel ID="Panel2" runat="server" Visible="False">
                                <asp:Label ID="lblReqNarr" runat="server" Text="Narration:" CssClass="lblName lblTxt"></asp:Label>
                                <asp:TextBox ID="txtMSRNarr" runat="server" Width="322px" CssClass="inputtextbox" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

