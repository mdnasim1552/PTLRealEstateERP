<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MktEmpKpiEntry.aspx.cs" Inherits="RealERPWEB.F_21_MKT.MktEmpKpiEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            var gvclient = $('#<%=this.gvclient.ClientID %>');

            //gvclient.gridviewScroll({
            //    width: 1160,
            //    height: 420,
            //    arrowsize: 30,
            //    railsize: 16,
            //    barsize: 8,
            //    varrowtopimg: "../Image/arrowvt.png",
            //    varrowbottomimg: "../Image/arrowvb.png",
            //    harrowleftimg: "../Image/arrowhl.png",
            //    harrowrightimg: "../Image/arrowhr.png",
            //    freezesize: 10
            //});


            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
            $('.chosen-continer').css('width', '600px');


            $('.chzn-select').chosen({ search_contains: true });
        }

        <%--  function SearchSalesTeam() {

            var srchteam = "%" + $('#<%=this.txtSrchSalesTeam.ClientID%>').val() + "%";
            var userid = $('#<%=this.lbluseid.ClientID%>').text().trim();
            var objsalesteam = new RealERPScript();
            var lst = objsalesteam.GetEmpCode(srchteam, userid);
            var ddlemployee = $('#<%=this.ddlEmpid.ClientID %>');
            ddlemployee.children('option').remove();
            $.each(lst, function (index, lst) {
                ddlemployee.append('<option value="' + lst.empid + '">' + lst.empname + '</option>');
            });--%>
        //  }

        function AddButton(id) {
            $(".hiddenb" + id).css("display", "inline");

        }
        function HiddenButton(id) {
            $(".hiddenb" + id).css("display", "none");
        }

        function AddButtonsub(id) {
            $(".hiddensub" + id).css("display", "inline");

        }
        function HiddenButtonsub(id) {
            $(".hiddensub" + id).css("display", "none");
        }
        function openModal() {
            //    $('#myModal').modal('show');
            $('#contact').modal('toggle');
        }

        function CloseModal() {

            $('#contact').modal('hide');
        }
    </script>

    <style>
        .ddmlist .btn-group button {
            width: 630px;
        }

        .ddmlist .multiselect-container {
            width: 100%;
            overflow-y: scroll !important;
            max-height: 300px !important;
        }

        .chzn-container-multi .chzn-choices .search-field .default {
            color: #999;
            height: 30px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .checkbox label {
            margin-left: 5px;
            padding-right: 5px;
        }
    </style>
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

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="lblDatefrm">Date</label>

                                <asp:TextBox ID="txtFrom" runat="server" CssClass="  inputDateBox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFrom"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="lblSalesTeam">Emp. Name</label>


                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">

                                <asp:DropDownList ID="ddlEmpid" runat="server" CssClass="chzn-select  form-control"
                                    TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="lblClientName">Clients Name </label>


                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">

                                <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="True" CssClass="chzn-select  form-control"
                                    TabIndex="5">
                                </asp:DropDownList>

                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary " TabIndex="9"></asp:LinkButton>
                                <asp:Label ID="lbluseid" runat="server" CssClass="lblTxt lblName " Style="display: none;"></asp:Label>

                                <asp:Label ID="lblEdit" runat="server" CssClass="lblTxt lblName " Style="display: none;"></asp:Label>



                                <%--  <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="lnkInteface" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/RptCashFlow.aspx">Transactions</asp:HyperLink>





                                    </div>
                                </div>
                            </div>--%>
                            </div>




                        </div>

                     




                    </div>



                    <div class="row">

                        <asp:MultiView ID="MultiView1" runat="server">

                            <asp:View ID="viewEmp" runat="server">

                                <%--<asp:GridView ID="gvInfo" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" PageSize="25" ShowFooter="true" Width="600px"
                                    CssClass="table table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcGrp" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc"))  + "</B>" %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkTotal_Click">Total :</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                    Width="2px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatPerInfo" runat="server" OnClick="Modalupdate_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtgvVal" runat="server" BorderWidth="0" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="320px"></asp:TextBox>

                                                <asp:TextBox ID="txtgvdVal" runat="server" BorderWidth="0" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="320px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy HH:mm" TargetControlID="txtgvdVal"></cc1:CalendarExtender>





                                                <asp:Panel ID="PnlProject" runat="server">

                                                    <asp:TextBox ID="txtProSearch" runat="server" Visible="false" CssClass="inputTxt inputName smltxtBox" TabIndex="10"></asp:TextBox>



                                                    <asp:LinkButton ID="imgSearchProject" Visible="false" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchProject_Click" TabIndex="11"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                                    <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" CssClass="chzn-select inputTxt form-control"
                                                        OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"
                                                        TabIndex="12">
                                                    </asp:DropDownList>


                                                </asp:Panel>

                                                <asp:Panel ID="PnlUnit" runat="server">

                                                    <asp:TextBox ID="txtUnitSearch" runat="server" Visible="false" CssClass="inputTxt inputName smltxtBox" TabIndex="10"></asp:TextBox>



                                                    <asp:LinkButton ID="imgSearchUnit" Visible="false" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchUnit_Click" TabIndex="11"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" CssClass="chzn-select inputTxt form-control"
                                                        OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged"
                                                        TabIndex="12">
                                                    </asp:DropDownList>

                                                </asp:Panel>
                                                <asp:Panel ID="pnlVisit" runat="server" Visible="false">
                                                    <asp:DropDownList ID="ddlVisit" Visible="false" runat="server" AutoPostBack="True" CssClass="chzn-select inputTxt form-control">
                                                    </asp:DropDownList>
                                                </asp:Panel>

                                                <asp:Panel ID="pnllocation" runat="server" Visible="false">
                                                    <asp:DropDownList ID="ddllocation" Visible="false" runat="server" AutoPostBack="True" CssClass="chzn-select inputTxt form-control"
                                                        TabIndex="12">
                                                    </asp:DropDownList>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlGrade" runat="server" Visible="false">
                                                    <asp:DropDownList ID="ddlgrade" Visible="false" runat="server" vidible="false" AutoPostBack="True" CssClass="chzn-select inputTxt form-control"></asp:DropDownList>
                                                </asp:Panel>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>--%>



                                <asp:GridView ID="gvInfo" runat="server" AllowPaging="True" OnRowDataBound="gvInfo_RowDataBound"
                                    AutoGenerateColumns="False" PageSize="25" ShowFooter="true"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                                <asp:Label ID="lblgvTime" runat="server" BorderWidth="0" BackColor="Transparent" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtime")) %>'></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcGrp" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc"))  + "</B>" %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <%-- <FooterTemplate>
                                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkTotal_Click">Total :</asp:LinkButton>

                                                </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle VerticalAlign="Middle" Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                    Width="5px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatPerInfo" runat="server" OnClick="Modalupdate_Click" CssClass="btn btn-danger  btn-sm ">Final Update</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtgvVal" runat="server" BorderWidth="0" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>




                                                <asp:TextBox ID="txtgvdVal" runat="server" BorderWidth="0" Style="width: 80px; float: left;" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>

                                                <asp:Panel ID="pnlTime" runat="server" Visible="false">
                                                    <asp:DropDownList ID="ddlhour" runat="server" CssClass="inputTxt ddlPage" Style="width: 50px; line-height: 22px;">
                                                        <asp:ListItem Value="01">01</asp:ListItem>
                                                        <asp:ListItem Value="02">02</asp:ListItem>
                                                        <asp:ListItem Value="03">03</asp:ListItem>
                                                        <asp:ListItem Value="04">04</asp:ListItem>
                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                        <asp:ListItem Value="06">06</asp:ListItem>
                                                        <asp:ListItem Value="07">07</asp:ListItem>
                                                        <asp:ListItem Value="08">08</asp:ListItem>
                                                        <asp:ListItem Value="09" Selected="True">09</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlMmin" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                                        <asp:ListItem Value="00">00</asp:ListItem>
                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="35">35</asp:ListItem>
                                                        <asp:ListItem Value="40">40</asp:ListItem>
                                                        <asp:ListItem Value="45">45</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="55">55</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlslb" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                                        <asp:ListItem Value="AM">AM</asp:ListItem>
                                                        <asp:ListItem Value="PM">PM</asp:ListItem>


                                                    </asp:DropDownList>



                                                </asp:Panel>
                                                <asp:Panel ID="pnlStatus" runat="server" Visible="false">


                                                    <asp:CheckBoxList ID="ChkBoxLstStatus" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                        runat="server" CssClass="form-control checkbox">
                                                    </asp:CheckBoxList>

                                                </asp:Panel>

                                                <asp:Panel ID="pnlParic" runat="server" Visible="false">
                                                    <asp:ListBox ID="ddlPartic" runat="server" SelectionMode="Multiple"
                                                        data-placeholder="Choose Participant......" multiple="true" class="form-control chosen-select"></asp:ListBox>

                                                </asp:Panel>

                                                <asp:Panel ID="PnlProject" runat="server">
                                                    <asp:DropDownList ID="ddlProject" runat="server"  CssClass="chzn-select inputTxt form-control"
                                                      
                                                        TabIndex="12">
                                                    </asp:DropDownList>
                                                </asp:Panel>
                                                <asp:Panel ID="PnlUnit" runat="server">
                                                    <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" CssClass="chzn-select inputTxt form-control"
                                                        OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged"
                                                        TabIndex="12">
                                                    </asp:DropDownList>
                                                </asp:Panel>

                                              
                                                  <asp:Panel ID="pnlVisit" runat="server" Visible="false">
                                                    <asp:DropDownList ID="ddlVisit" Visible="false" runat="server" AutoPostBack="True" CssClass="chzn-select inputTxt form-control">
                                                    </asp:DropDownList>
                                                </asp:Panel>

                                                    <asp:Panel ID="pnlFollow" runat="server" Visible="false">
                                                        <%-- <asp:DropDownList ID="ddlFollow" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control">
                                                        </asp:DropDownList>--%>



                                                        <asp:CheckBoxList ID="ChkBoxLstFollow" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                            runat="server" CssClass="form-control checkbox">
                                                        </asp:CheckBoxList>


                                                    </asp:Panel>
                                             

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Width="600px" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>

                                <div class=" col-md-12 pading5px">
                                    <asp:Label ID="lblHeaderPredis" runat="server" CssClass="smLbl_to pading5px" Text=" Previous Discussion" Visible="false"></asp:Label>

                                </div>
                                <div class="clearfix"></div>

                                <div class="table-responsive">
                                     <asp:GridView ID="gvclient" runat="server"
                                    AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                    CssClass="table-striped table-hover table-bordered  grvContentarea"  OnRowDataBound="gvclient_RowDataBound" >
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />

                                    <Columns>

                                        
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" Font-Bold="True" Height="16px" ToolTip="Delete" Style="text-align: right" Text="Delete" OnClientClick="javascript:return  FunConfirm()" OnClick="lbtnDelete_Click"><span  style="color:red;"  class=" fa fa-recycle"></span></asp:LinkButton>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />



                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" ToolTip="Edit" Text="Edit" OnClientClick="javascript:return  FunConfirmEdit()" OnClick="lbtnEdit_Click"> <span class=" fa   fa-edit"></span></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />



                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Meeting </br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMeetingdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                    Width="110px"></asp:Label>

                                                <asp:Label ID="lblgvDate" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                      

                                        <asp:TemplateField HeaderText="Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvkpigrp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followupdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                          <asp:TemplateField HeaderText="Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvprodesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>

                                                <asp:Panel ID="pnldis" runat="server" ClientIDMode="Static">

                                                <asp:Label ID="lgvDiscussion0"   runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                    Width="190px">                                             


                                                </asp:Label>
                                                <asp:LinkButton ID="lnkAdddis" ClientIDMode="Static"   Width="10"  ToolTip="Comments" runat="server" OnClick="lnkAdddis_Click"><span class="fa fa-edit"></span></asp:LinkButton>
                                               
                                                </asp:Panel>
                                                 <asp:Label ID="lblgvdisgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "disgnote")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="Participants">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpartcilist" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next </br>Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnfollowup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Next </br>Appointment">
                                            <ItemTemplate>
                                                <asp:Label ID="nappdat0" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                    Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Panel ID="pnlsub" runat="server" ClientIDMode="Static" >
                                                <asp:Label ID="lgvndissub" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                    Width="150px"></asp:Label>
                                                <asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server"  OnClick="lnkAdddissub_Click" ><span class="fa fa-edit"></span></asp:LinkButton>
                                                
                                                    </asp:Panel>
                                                    <asp:Label ID="lblgvsubgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subgnote")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left"  />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="L.Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlqstatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lqstatus")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlstatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>

                                   
                                </div>
                            </asp:View>

                        </asp:MultiView>

                    </div>
                </div>
            </div>



               <div class="modal fade right" id="contact" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                            aria-hidden="true" data-backdrop="false">
                            <div class="modal-dialog  modal-lg  modal-side modal-bottom-right modal-notify modal-info" role="document">
                                <!--Content-->
                                <div class="modal-content">
                                    <!--Header-->
                                    <div class="modal-header">
                                        <p class="heading">
                                            <h4 id="lblheader" runat="server"><span class="glyphicon glyphicon-info-sign"></span></h4>
                                        </p>

                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true" class="white-text">&times;</span>
                                        </button>
                                    </div>

                                    <!--Body-->
                                    <div class="modal-body">

                                        <div class="row">

                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label  id="lbldsi" runat="server" class="control-label lblmargin-top9px"></label>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lbldiscussion" runat="server" CssClass="form-control " TextMode="MultiLine" Height="100px" style="background:#DFF0D8"></asp:Label>


                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label for="lblcomm" class="control-label lblmargin-top9px">Comments:</label>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtComm" runat="server" CssClass="form-control " TextMode="MultiLine" Height="100px"></asp:TextBox>


                                                </div>
                                            </div>


                                            <asp:Panel ID="pnld" runat="server" Visible="false">

                                            <asp:Label ID="lblEmpid" runat="server"></asp:Label>
                                            <asp:Label ID="lblclient" runat="server"></asp:Label>
                                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                                           

                                            </asp:Panel>


                                        </div>
                                    </div>

                                    <!--Footer-->
                                    <div class="modal-footer">


                                        <asp:LinkButton ID="lUpdatInfo" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClientClick="CloseModal();" OnClick="lUpdatInfo_Click" >Save</asp:LinkButton>

                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true" class="white-text">&times;</span>
                                        </button>

                                    </div>
                                </div>
                                <!--/.Content-->
                            </div>
                        </div>

            <script>
                function Exit() {
                    var x = confirm('Are You sure want to exit:');
                    if (x) window.close();
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


