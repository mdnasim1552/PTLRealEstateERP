<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptDuesReportAll.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptDuesReportAll" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            //alert("I m IN");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {


            try {
                var gv1 = $('#<%=this.dgv1.ClientID %>');
                gv1.Scrollable();
                
                var gvtbcon = $('#<%=this.gvtbcon.ClientID %>');

                var gvPrjtrbal = $('#<%=this.gvPrjtrbal.ClientID %>');
                var grvTrBal2 = $('#<%=this.grvTrBal2.ClientID %>');
                var gvprjtbal03 = $('#<%=this.gvprjtbal03.ClientID %>');

               

               
                gvtbcon.Scrollable();


                gvPrjtrbal.Scrollable();
                grvTrBal2.Scrollable();
                gvprjtbal03.Scrollable();

                $('.chzn-select').chosen({ search_contains: true });

            }

            catch (e) {

                alert(e.message)
            }


        }

    </script>
    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }


        .grvContentarea > thead > tr > th, .grvContentarea > tbody > tr > th, .grvContentarea > tfoot > tr > th, .grvContentarea > thead > tr > td, .grvContentarea > tbody > tr > td, .grvContentarea > tfoot > tr > td {
            padding: 0 2px 0 1px;
        }

        .PopCal {
            z-index: 10005;
        }
        tabMenu a {
            display: block;
            line-height: 15px;
            font-size: 14px;
            color: #000;
            text-align: center;
            background: #fff;
        }
        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                width: 140px;
                padding: 0px 0;
                float: left;
                list-style: none;
                margin: 0 2px;
                color: #fff;
                background: #5F5F5F;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

                ul.tbMenuWrp li a {
                    padding: 0 0;
                    background: #5F5F5F;
                    -webkit-border-radius: 4px;
                    -moz-border-radius: 4px;
                    border-radius: 4px;
                    display: block;
                    color: #fff;
                    padding: 0px 0 0 0;
                    vertical-align: middle;
                    border: none !important;
                }

                    ul.tbMenuWrp li a:hover {
                        background: #12A5A6;
                    }

                    ul.tbMenuWrp li a:focus {
                        outline: none;
                        outline-offset: 0;
                    }

                    ul.tbMenuWrp li a label {
                        color: #fff;
                        background: none;
                        border: none;
                        text-align: center;
                        font-weight: bold;
                        font-size: 16px;
                        display: block;
                        cursor: pointer;
                        width: 100%;
                    }

        .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > a:hover {
            background: #472AC6 !important;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 3px;
            color: #fff;
            text-align: center;
            cursor: pointer;
            background: #fff;
            position: relative;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 35px;
            margin: 1px 0;
            width: 100%;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
             
            }


        .tbMenuWrp table tr td input[type="checkbox"], .tbMenuWrp table tr td input[type="radio"] {
            display: none;
        }

       

        .tbMenuWrp table tr td label span.lbldata {
            border: 2px solid #fff;
            border-radius: 50%;
            color: #fff;
            display: inline-block;
            float: left;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
            position: absolute;
            right: 4px;
            top: 7px;
        }

     

        .tbMenuWrp table tr td label .lblactive {
            background: #667DE8;
            color: #000000;
            
        }
        .lblactive label tr td {
            background: #667DE8 !important;
            color: #000 !important;
            
        }
        .blink_me {
            animation: blinker 5s linear infinite;
            
        }
        .circle-tile {
            margin-bottom: 15px;
            text-align: center;
            width: 125px;
            font-size: 18px;
        }

        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 15px;
            height: 39px;
            margin: -2px auto -22px;
            padding: 4px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 42px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 8px;
            padding-bottom: 6px;
        }

        .circle-tile-number {
            font-size: 26px;
            font-weight: 700;
            line-height: 1;
            padding: 5px 0 15px;
        }

        .circle-tile-description {
            text-transform:capitalize;
        }

        .circle-tile-footer {
            background-color: rgba(0, 0, 0, 0.1);
            color: rgba(255, 255, 255, 0.5);
            display: block;
            padding: 5px;
            transition: all 0.3s ease-in-out 0s;
        }

            .circle-tile-footer:hover {
                background-color: rgba(0, 0, 0, 0.2);
                color: rgba(255, 255, 255, 0.5);
                text-decoration: none;
            }

        .circle-tile-heading.dark-blue:hover {
            background-color: #2E4154;
        }

        .circle-tile-heading.green:hover {
            background-color: #138F77;
        }

        .circle-tile-heading.orange:hover {
            background-color: #DA8C10;
        }

        .circle-tile-heading.blue:hover {
            background-color: #2473A6;
        }

        .circle-tile-heading.red:hover {
            background-color: #CF4435;
        }

        .circle-tile-heading.purple:hover {
            background-color: #7F3D9B;
        }
        
        .text-faded {
            color:#000;
        }
        .common_color {
            background-color:#faf5f5;
        }
    
    </style>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
            <div class=" card card-fluid">
                <div class=" card-body" style="min-height: 250px;">
                    <div class="row">
                        <fieldset class="tabMenu">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="tbMenuWrp nav nav-tabs">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Mains"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Current Dues</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="AsOnDues"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'> As On Dues</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="Trial02"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'></div></div></div></asp:ListItem>
                                            <asp:ListItem Value="HOTB"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'></div></div></div></asp:ListItem>

                                            <asp:ListItem Value="PrjTrailBal"><div class='circle-tile '><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'></div></div></div></asp:ListItem>                                           
                                             <asp:ListItem Value="TrailBal2"><div class='circle-tile '><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'></div></div></div></asp:ListItem>
                                            <asp:ListItem Value="LandPrj"><div class='circle-tile '><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'></div></div></div></asp:ListItem>
                                            <asp:ListItem Value="PrjTrailBal3"  style="display:none;"><div class='circle-tile  pull-right'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'></div></div></div></asp:ListItem>

                                        </asp:RadioButtonList>

                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </fieldset>

                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="TrialBalance" runat="server">

                            <div class="card card-fluid" >
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label  lblmargin-top9px" for="FromDate" id="lblDaterange" runat="server">From</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>

                                            </div>

                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label lblmargin-top9px" for="lblDateto" id="lblDateto" runat="server">To Date</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>


                                            </div>
                                        </div>

                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label lblmargin-top9px" for="lblreportlevel">Project Name :</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="custom-select  chzn-select">
                                                  
                                                </asp:DropDownList>


                                            </div>

                                        </div>



                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary" OnClick="lnkok_Click">Ok</asp:LinkButton>
                                            </div>
                                        </div>
                                      

                                        
                                 <asp:RadioButtonList ID="rbtntype" RepeatDirection="Horizontal" CssClass=""  runat="server">

                                   
                                   <asp:ListItem Value ="Booking" >Booking Dues</asp:ListItem>
                                  <asp:ListItem Value="CRDUES"> CR Dues</asp:ListItem>                               
                                  <asp:ListItem Selected="True">Both</asp:ListItem>
                                </asp:RadioButtonList>

                               


                                       
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="msgHandSt">
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                                    <ProgressTemplate>
                                                        <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar"
                                                            Text="Please Wait........"></asp:Label>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                            </div>


                                        </div>
                                    </div>





                                </div>
                            </div>


                            <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered  grvContentarea"
                                 ShowFooter="True"
                                PageSize="15">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="33px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>' Width="95px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>




                                    <asp:TemplateField FooterText="Total"
                                        HeaderText="Project Name">
                                        <HeaderTemplate>

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Description Of Accounts" Width="180px"></asp:Label>


                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel"></span></asp:HyperLink>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvDesc" runat="server"
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="300px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAcDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>' Width="120px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Unitname")) %>' Width="120px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderText="Installment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvInsment" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' Width="150px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Schedule Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSchdate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Booking Dues">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfbookdues" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbookdue" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="CR Dues">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfCRdues" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCRdues" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </asp:View>

                        <asp:View ID="ProTrailBal" runat="server">

                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label  lblmargin-top9px" for="FromDate" id="Label1" runat="server">From</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <asp:TextBox ID="txtDatefromP" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1_txtDatefromP" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefromP"></cc1:CalendarExtender>

                                            </div>

                                        </div>
                                       
                                       <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label lblmargin-top9px" for="lblreportlevel">Project</label>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                 <asp:DropDownList ID="ddlProjectInd" runat="server" CssClass="custom-select  chzn-select">
                                           
                                        </asp:DropDownList>
                                                


                                            </div>

                                        </div>

                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label lblmargin-top9px" for="lblreportlevel">Group</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                 <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="custom-select  chzn-select">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                                


                                            </div>

                                        </div>



                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="btnPrjTr" runat="server" CssClass="btn btn-primary" OnClick="btnPrjTr_Click">Ok</asp:LinkButton>
                                            </div>
                                        </div>
                                       
                                    </div>
                                    





                                </div>
                            </div>


                                <asp:GridView ID="gvPrjtrbal" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvPrjtrbal_RowDataBound" ShowFooter="True" Width="658px">
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
                                                <asp:Label ID="lblgvCode" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Description" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDesc" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                                           %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Description">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label></td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                           
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Debit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCre" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </asp:View>
                        
                        <asp:View ID="TrailsBal2" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label  lblmargin-top9px" for="FromDate" id="Label8" runat="server">From</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <asp:TextBox ID="txtDatefromT2" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>

                                            </div>

                                        </div>
                                       




                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="btmt2" runat="server" CssClass="btn btn-primary" OnClick="btmt2_Click">Ok</asp:LinkButton>
                                            </div>
                                        </div>
                                       
                                    </div>
                                    





                                </div>
                            </div>

                                <asp:GridView ID="grvTrBal2" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="grvTrBal2_RowDataBound" ShowFooter="True" Width="658px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCode" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2")) %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText=" Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc2").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc2")).Trim(): "")  %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Debit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTDrAmt" runat="server" ForeColor="White" Font-Size="12px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCre" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCrAmt" runat="server" ForeColor="White" Font-Size="12px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </asp:View>

                        <asp:View ID="ViewConsolidated" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">

                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label13" runat="server" CssClass="control-label lblmargin-top9px" Text="As On Date"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtAsDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtAsDate" Enabled="true"></cc1:CalendarExtender>


                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label15" runat="server" CssClass="control-label lblmargin-top9px" Text="Project Name :"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlProjectName2" runat="server" CssClass="custom-select chzn-select" TabIndex="6">
                                                   
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkTrialBalCon" runat="server" CssClass="btn btn-primary
"
                                                    OnClick="lnkTrialBalCon_Click">Ok</asp:LinkButton>

                                            </div>
                                        </div>

                                         <asp:RadioButtonList ID="rbtntype1" RepeatDirection="Horizontal" CssClass=""  runat="server">

                                   
                                   <asp:ListItem Value ="Booking" Selected="True">Booking Dues</asp:ListItem>
                                  <asp:ListItem Value="CRDUES"> CR Dues</asp:ListItem>                               
                                <%--  <asp:ListItem Selected="True">Both</asp:ListItem>--%>
                                </asp:RadioButtonList>


                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <div class="msgHandSt">


                                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" DisplayAfter="50">
                                                        <ProgressTemplate>
                                                            <asp:Label ID="Label4" runat="server" CssClass="lblProgressBar"
                                                                Text="Please Wait.........."></asp:Label>

                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>

                                                </div>


                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <asp:GridView ID="gvtbcon" runat="server" AutoGenerateColumns="False"
                                            CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                            PageSize="20" ShowFooter="True">
                                            <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoidas" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="33px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcodeas" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>' Width="95px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>




                                    <asp:TemplateField FooterText="Total"
                                        HeaderText="Project Name">
                                        <HeaderTemplate>

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Description Of Accounts" Width="180px"></asp:Label>


                                            <asp:HyperLink ID="hlbtntbCdataExelas" runat="server"
                                                CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel"></span></asp:HyperLink>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvDescas" runat="server"
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="300px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAcDescas" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>' Width="120px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUDescas" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Unitname")) %>' Width="120px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderText="Installment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvInsmentas" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' Width="150px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Schedule Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSchdateas" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Booking Dues" Visible="false">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfBookduesas" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBookdueas" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                           <asp:TemplateField HeaderText="CR Dues" Visible="false">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfCurrentduesas" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCurrentdueas" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    


                                </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>


                                </div>
                            </div>
                        </asp:View>


                        <asp:View ID="viewProjectTriabalance03" runat="server">

                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label  lblmargin-top9px" for="FromDate" id="Label3" runat="server">From</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <asp:TextBox ID="txtDatefromT3" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefromT3"></cc1:CalendarExtender>

                                            </div>

                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label lblmargin-top9px" for="lblDateto" id="Label6" runat="server">To</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                            </div>
                                        </div>

                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label lblmargin-top9px" for="lblreportlevel">Reports</label>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">

                                                 <asp:DropDownList ID="ddlProjectInd2" runat="server" CssClass="custom-select  chzn-select">
                                               
                                                     </asp:DropDownList>

                                            </div>

                                        </div>



                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="btmt3" runat="server" CssClass="btn btn-primary" OnClick="btmt3_Click">Ok</asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <label id="Label7" runat="server" class="switch">
                                                <asp:CheckBox ID="chkdetails" runat="server"  />
                                                <span class="btn btn-xs slider round"></span>
                                            </label>
                                            <asp:Label runat="server" Text="Details" ID="Label9" CssClass="control-label"></asp:Label>

                                        </div>
                                    </div>
                                    





                                </div>
                            </div>

                                <asp:GridView ID="gvprjtbal03" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvprjtbal03_RowDataBound" ShowFooter="True" Width="658px">
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
                                                <asp:Label ID="lblgvCodetp" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText=" Description">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4tp" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label></td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExeltp" runat="server"
                                                                CssClass=" btn btn-danger btn-xs  fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesctp" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+  
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="300px">




                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="totaldb" runat="server" Font-Bold="True"
                                                                Text="Total Debit=" Width="180px"></asp:Label>

                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="totalcr" runat="server" Font-Bold="True"
                                                                Text="Total Credit=" Width="180px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="netamnt" runat="server" Font-Bold="True"
                                                                Text="Net Amount=" Width="180px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        

                                        <asp:TemplateField HeaderText="Opening(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvopnamtp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="lgvFopdbamt" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>

                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="lgvFopCramt" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                                <asp:Label ID="lgvFopnamtp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Debit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmttp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTDrAmttp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCreamttp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCrAmttp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing(in Tk.)">
                                            <ItemTemplate>

                                                <asp:Label ID="lgvClsamtp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="lgvFoClsdbamt" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style61">
                                                            <asp:Label ID="lgvFoClsCramt" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                                <asp:Label ID="lgvFClsamtp" runat="server" Font-Size="12px" Style="text-align: left"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </asp:View>
                        

                        
                    

                        
                        
                        <asp:View ID="Trialbanlace01" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label23" runat="server" CssClass="control-label lblmargin-top9px" Text="From"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatefromtb" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender9" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefromtb" Enabled="true"></cc1:CalendarExtender>


                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label24" runat="server" CssClass="control-label lblmargin-top9px" Text="To"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatetotb" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender10" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatetotb" Enabled="true"></cc1:CalendarExtender>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label25" runat="server" CssClass="control-label lblmargin-top9px" Text="Report Level"></asp:Label>

                                            </div>

                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlReportLeveltb2" runat="server" CssClass="custom-select" TabIndex="6">
                                                    <asp:ListItem Value="1">Level-1</asp:ListItem>
                                                    <asp:ListItem Value="2">Level-2</asp:ListItem>
                                                    <asp:ListItem Value="3">Level-3</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="4">Level-4</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkTrial02" runat="server" CssClass="btn btn-primary" OnClick="lnkTrial02_Click">Ok</asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:UpdateProgress ID="UpdateProgress10" runat="server" DisplayAfter="50">
                                                    <ProgressTemplate>
                                                        <asp:Label ID="Label2pbar" runat="server" CssClass="lblProgressBar"
                                                            Text="Please Wait.........."></asp:Label>

                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                        </div>


                                    </div>

                                    <asp:GridView ID="gvtrialbalance01" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" OnRowDataBound="gvtrialbalance01_RowDataBound"
                                        PageSize="20">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblserialnoid01" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="33px"></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcode01" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>' Width="95px"></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Decription" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAcDesc01" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>' Width="95px"></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <%--                                    <asp:TemplateField FooterText="Total"
                                        HeaderText="Description of Accounts">
                                        <HeaderTemplate>
                                            <table style="width: 300px;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                            Text="Description Of Accounts" Width="180px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" 
                                                              CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvDesc" runat="server" 
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                Width="300px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Description Of accounts">
                                                <HeaderTemplate>
                                                    <table style="width: 47%;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label312201" runat="server" Font-Bold="True"
                                                                    Text="Description Of Accounts" Width="180px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>


                                                            <td class="style60">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;</td>
                                                            <td>

                                                                <asp:HyperLink ID="hlbtntbCdataExel01" CssClass="btn btn-xs btn-success" runat="server"><span class="fa fa-file-excel-o"></span></asp:HyperLink>

                                                                <%--   <asp:HyperLink ID="hlbtntbCdataExel" runat="server" 
                                                             CssClass=" btn btn-danger btn-xs"   Font-Bold="true"  BackColor="#000066" BorderColor="White" BorderWidth="1px" BorderStyle="Solid" Text="Export to Excel"></asp:HyperLink>--%>
                                                            </td>



                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvDesc01" runat="server" __designer:wfdid="w38"
                                                        CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")).Trim().Length>0 ?  "<br>" : "")+                                                           
                                                                        
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="400px">


                                                    </asp:HyperLink>



                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" Res Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtrial7" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Opening <br/> Dr. Amt">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfopndramt01" runat="server" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvopndramt01" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField
                                                HeaderText="Opening <br/> Cr. Amt">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfopncramt01" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvopncramt01" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opncram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Dr. Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfDramt01" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDramt01" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr. Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfCramt01" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCramt01" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Closing  <br/> Dr.  Amt">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfclodramt01" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvclodramt01" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Closing    <br/> Cr. Amt">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfclocramt01" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvclocramt01" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Net Amt">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfnetamt01" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnetamt01" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdrcr01" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "drcr")) %>' Width="20px"></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </asp:View>

                    </asp:MultiView>

                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->
            <asp:MultiView ID="MultiViewold" runat="server">
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

