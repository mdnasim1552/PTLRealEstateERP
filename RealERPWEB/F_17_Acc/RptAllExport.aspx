<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAllExport.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAllExport" %>

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
                var gv2 = $('#<%=this.gvDetails.ClientID %>');
                var gv3 = $('#<%=this.gvBankPosition.ClientID %>');
                var gvtbcon = $('#<%=this.gvtbcon.ClientID %>');
                var dgvIS = $('#<%=this.dgvIS.ClientID %>');

              //  gv1.Scrollable();

                gv2.Scrollable();
                gv3.Scrollable();
                gvtbcon.Scrollable();
                dgvIS.Scrollable();

                //$("input, select").bind("keydown", function (event) {
                //    var k1 = new KeyPress();
                //    k1.textBoxHandler(event);

                //});

            }

            catch (e)
            {

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

        .PopCal { z-index: 10005; }
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
                <div class=" card-body" style="min-height:250px;">

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="TrialBalance" runat="server">

                            <div class="card card-fluid" >
                                <div class="card-body" >
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label  lblmargin-top9px" for="FromDate" id="lblDaterange" runat="server">From</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <cc1:CalendarExtender  ID="txtDatefrom_CalendarExtender" runat="server" 
                                                    Format="dd-MMM-yyyy"  TargetControlID="txtDatefrom" >

                                                </cc1:CalendarExtender>
                                                 
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
                                                <label class="control-label lblmargin-top9px" for="lblreportlevel">Reports</label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <asp:DropDownList ID="ddlReportLevel" runat="server" CssClass="custom-select  chzn-select">
                                                    <asp:ListItem Value="1">Level-1</asp:ListItem>
                                                    <asp:ListItem Value="2">Level-2</asp:ListItem>
                                                    <asp:ListItem Value="3">Level-3</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="4">Level-4</asp:ListItem>
                                                </asp:DropDownList>


                                            </div>

                                        </div>



                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary" OnClick="lnkok_Click">Ok</asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <label id="chkbod" runat="server" class="switch">
                                                <asp:CheckBox ID="chknetbalance" runat="server" AutoPostBack="true"  OnCheckedChanged="chknetbalance_CheckedChanged"/>
                                                <span class="btn btn-xs slider round"></span>
                                            </label>
                                            <asp:Label runat="server" Text="Net Balance" ID="lblnetbalance" CssClass="control-label"></asp:Label>

                                        </div>
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
                                            OnRowDataBound="dgv1_RowDataBound" ShowFooter="True"
                                            PageSize="15">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="33px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>' Width="95px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Decription" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAcDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>' Width="95px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField FooterText="Total"
                                                    HeaderText="Description of Accounts">
                                                    <HeaderTemplate>

                                                         <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Description Of Accounts" Width="180px"></asp:Label>


                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                                        CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                                     
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
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Opening <br/> Dr. Amt">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfopndramt" runat="server" Font-Bold="True"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvopndramt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField
                                                    HeaderText="Opening <br/> Cr. Amt">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfopncramt" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvopncramt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opncram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Dr. Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfDramt" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDramt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cr. Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfCramt" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCramt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Closing  <br/> Dr.  Amt">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfclodramt" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvclodramt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Closing    <br/> Cr. Amt">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfclocramt" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvclocramt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Net Amt">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfnetamt" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvnetamt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdrcr" runat="server"
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
                        </asp:View>

                        <asp:View ID="DetailsTrial" runat="server">
                            <div class="card card-fluid" style="min-height: 400px;">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" CssClass="control-label  lblmargin-top9px" Text="From"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatefromd" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefromd" Enabled="true"></cc1:CalendarExtender>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label8" runat="server" CssClass="control-label lblmargin-top9px" Text="To"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatetod" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatetod" Enabled="true"></cc1:CalendarExtender>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="lblAccLevel" runat="server" CssClass="control-label lblmargin-top9px" Text="Level"></asp:Label><br />
                                                (Accounts)
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlacclevel" runat="server" CssClass="custom-select" TabIndex="6">
                                                    <asp:ListItem Value="2">Level-1</asp:ListItem>
                                                    <asp:ListItem Value="4">Level-2</asp:ListItem>
                                                    <asp:ListItem Value="8" Selected="True">Level-3</asp:ListItem>
                                                    <asp:ListItem Value="12">Details</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkDetailsok" runat="server" CssClass="btn btn-primary" OnClick="lnkDetailsok_Click">Ok</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" CssClass="control-label lblmargin-top9px" Text="Level"></asp:Label><br />
                                                (Resource)

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlReportLevelDetails" runat="server" CssClass="custom-select" TabIndex="6">
                                                    <asp:ListItem Value="2">Main</asp:ListItem>
                                                    <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                                    <asp:ListItem Value="7">Sub-2</asp:ListItem>
                                                    <asp:ListItem Value="9">Sub-3</asp:ListItem>
                                                    <asp:ListItem Value="12" Selected="True">Details</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <div class="msgHandSt">


                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="50">
                                                        <ProgressTemplate>
                                                            <asp:Label ID="Label34" runat="server" CssClass="lblProgressBar"
                                                                Text="Please Wait.........."></asp:Label>

                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>

                                                </div>

                                            </div>
                                        </div>


                                    </div>
                                    <div class="row">

                                        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False"
                                            CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                            OnRowDataBound="gvDetails_RowDataBound" ShowFooter="True"
                                            PageSize="15">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcoded" runat="server" CssClass="GridLebelL"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rescode4").ToString().Trim().Length>0? 
                                                                   (Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode4")).Trim(): "") 
                                                                          %>'
                                                            Width="200px">
                                                                          
                                                        </asp:Label>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right"
                                                    HeaderText="Description">
                                                    <HeaderTemplate>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td class="style58">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description "></asp:Label>
                                                                </td>
                                                                <td class="style59">&nbsp;</td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtnCdataExel" runat="server" BackColor="#000066"
                                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                        ForeColor="White">Export Exel</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvdescriptiond" runat="server" CssClass="GridLebelL"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                          %>'
                                                            Width="400px">
                                                             
                                                             
                                                        </asp:Label>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfopDes" runat="server" CssClass="GridLebel"
                                                            Font-Bold="True"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt"
                                                    ItemStyle-HorizontalAlign="Right">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfopnamtd" runat="server"
                                                            Font-Bold="True"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvopnamtd" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount"
                                                    ItemStyle-HorizontalAlign="Right">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfDramtd" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDramtd" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount"
                                                    ItemStyle-HorizontalAlign="Right">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfCramtd" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCramtd" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amount"
                                                    ItemStyle-HorizontalAlign="Right">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfcloamtd" runat="server" CssClass="GridLebel"
                                                            Font-Bold="True"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvclobald" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <RowStyle CssClass=" grvRows" />
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="ViewBankPosition" runat="server">

                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row ">

                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label  lblmargin-top9px" for="FromDate" id="lblDate" runat="server">From</label>

                                            </div>
                                        </div>


                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatefrombank" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender11" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefrombank"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label lblmargin-top9px" for="ToDate">To Date</label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <asp:TextBox ID="txtDatetobank" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender12" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatetobank"></cc1:CalendarExtender>

                                            </div>

                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label lblmargin-top9px" for="ddlUserName">Reports</label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <asp:DropDownList ID="ddlReportLevelBank" runat="server" CssClass="custom-select  chzn-select">
                                                    <asp:ListItem Value="2">Level-1</asp:ListItem>
                                                    <asp:ListItem Value="4">Level-2</asp:ListItem>
                                                    <asp:ListItem Value="8">Level-3</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkbtnBankPosition" runat="server" CssClass=" btn btn-primary" OnClick="lnkbtnBankPosition_Click">Ok</asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">


                                        <asp:GridView ID="gvBankPosition" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader" ShowFooter="True" OnRowDataBound="gvBankPosition_OnRowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblserialnoid1" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcodebank" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right"
                                                    HeaderText="Description of Accounts">
                                                    <HeaderTemplate>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td class="style58" style="width: auto">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description of Accounts"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:HyperLink ID="hlbtnbnkpdataExel" runat="server" BackColor="#000066"
                                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                        ForeColor="White">Export Exel</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HLgvDescbank" runat="server" __designer:wfdid="w38"
                                                            CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                            Width="220px"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Opening Balance"
                                                    ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvopnbal" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="92px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Opening Liabilities"
                                                    ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvopnliabilities" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opncram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="93px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Deposit"
                                                    ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDramtbank" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="92px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Withdrawn"
                                                    ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCramtbank" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="93px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Closing Balance"
                                                    ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvclobalbank" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="92px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Closing Liabilities"
                                                    ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcloliabilities" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="93px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>



                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Bank Limit"
                                                    ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbankLim" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                    FooterStyle-HorizontalAlign="Right" HeaderText="Available Balance"
                                                    ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbankBal" runat="server" CssClass="GridLebel"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankbal")).ToString("#,##0.00;(#,##0.00); ") %>' Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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
                                                <asp:Label ID="Label15" runat="server" CssClass="control-label lblmargin-top9px" Text="Report Level"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlReportLevelcon" runat="server" CssClass="custom-select" TabIndex="6">
                                                    <asp:ListItem Value="2">Level-1</asp:ListItem>
                                                    <asp:ListItem Value="4">Level-2</asp:ListItem>
                                                    <asp:ListItem Value="8">Level-3</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="12">Level-4</asp:ListItem>
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
                                        OnRowDataBound="gvtbcon_RowDataBound" PageSize="20" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblserialnoid2" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcodecon" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                        Width="95px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" FooterText="Total"
                                                HeaderText="Description of Accounts">
                                                <HeaderTemplate>
                                                    <table style="width: 47%;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label5" runat="server" Font-Bold="True"
                                                                    Text="Description Of Accounts" Width="180px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExelcon" runat="server" BackColor="#000066"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvDesccon" runat="server" __designer:wfdid="w38"
                                                        CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                        Width="300px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="left" />
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Closing <br /> Dr. Amount"
                                                ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfClosDramtcon" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgClosDramtcon" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Closing <br /> Cr. Amount"
                                                ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfClosCramtcon" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvClosCramtcon" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Net  &lt;br/&gt; Dr.  Amt"
                                                ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfnetdramtcon" runat="server" CssClass="GridLebel"
                                                        Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnetdramtcon" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netdram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Net  &lt;br/&gt; Cr. Amt"
                                                ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfnetcramtcon" runat="server" CssClass="GridLebel"
                                                        Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnetcramtcon" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netcram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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



                        <asp:View ID="ViewBankPos02" runat="server">                         

                                <div class="row">
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" CssClass="control-label lblmargin-top9px" Text="As On Date"></asp:Label>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtAsDateb" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtAsDateb" Enabled="true"></cc1:CalendarExtender>




                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label11" runat="server" CssClass="control-label lblmargin-top9px" Text="Report Level"></asp:Label>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlReportLevelbk02" runat="server" CssClass="custom-select" TabIndex="6">
                                                <asp:ListItem Value="2">Level-1</asp:ListItem>
                                                <asp:ListItem Value="4">Level-2</asp:ListItem>
                                                <asp:ListItem Value="8">Level-3</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="12">Level-4</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkBankPosition02" runat="server" CssClass="btn btn-primary" OnClick="lnkBankPosition02_Click">Ok</asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div class="msgHandSt">


                                                <asp:UpdateProgress ID="UpdateProgress5" runat="server" DisplayAfter="50">
                                                    <ProgressTemplate>
                                                        <asp:Label ID="Label5" runat="server" CssClass="lblProgressBar"
                                                            Text="Please Wait.........."></asp:Label>

                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                <asp:GridView ID="gvBankPosition02" runat="server" AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                    ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid3" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcodebank02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style58" style="width: auto">
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True"
                                                                Text="Description of Accounts"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnbnkpdataExel02" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDescbank02" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="400px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing Balance"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobalbank02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing Liabilities"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcloliabilities02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Issue Amt"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueamt" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Collection Amt"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcollamt" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Net Balance"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnetbal" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Bank Liabilities"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbankliabilities" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "banklia")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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
                        </asp:View>

                        <asp:View ID="ViewBalConfirmation" runat="server">

                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label17" runat="server" CssClass="control-label lblmargin-top9px" Text="From"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatefrombankcb" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender7" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefrombankcb" Enabled="true"></cc1:CalendarExtender>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label18" runat="server" CssClass="control-label lblmargin-top9px" Text="To"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatetobankcb" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender8" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatetobankcb" Enabled="true"></cc1:CalendarExtender>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label19" runat="server" CssClass="control-label" Text="Report Level"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlReportLevelBankcb" runat="server" CssClass="custom-select" TabIndex="6">
                                                    <asp:ListItem Value="2">Level-1</asp:ListItem>
                                                    <asp:ListItem Value="4">Level-2</asp:ListItem>
                                                    <asp:ListItem Value="8">Level-3</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkbtnCashBankBal" runat="server" CssClass="btn btn-primary" OnClick="lnkbtnCashBankBal_Click">Ok</asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:UpdateProgress ID="UpdateProgress6" runat="server" DisplayAfter="50">
                                                    <ProgressTemplate>
                                                        <asp:Label ID="Label6" runat="server" CssClass="lblProgressBar"
                                                            Text="Please Wait.........."></asp:Label>

                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                        </div>

                                    </div>
                                    <asp:GridView ID="gvCABankBal" runat="server" AutoGenerateColumns="False"
                                        CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                        ShowFooter="True" OnRowDataBound="gvCABankBal_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblserialnoid5" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcodebank4" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts">
                                                <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td class="style58" style="width: auto">
                                                                <asp:Label ID="Label9" runat="server" Font-Bold="True"
                                                                    Text="Description of Accounts"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtnbnkpdataExelcb" runat="server" BackColor="#000066"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White">Export Exel</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvDescbankcb" runat="server"
                                                        CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="250px">
                                                                      
                                                                      
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="left" />
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Change"
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnetbalcb" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Opening "
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvopnamcb" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Closing"
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvclosamcb" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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
                        </asp:View>
                        <%--DOne--%>

                        <asp:View ID="ViewBalDetails02" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label7" runat="server" CssClass="control-label lblmargin-top9px" Text="From"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatefrombdet2" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDatefrombdet2_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefrombdet2" Enabled="true"></cc1:CalendarExtender>


                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label20" runat="server" CssClass="control-label lblmargin-top9px" Text="To"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatetobdet2" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDatetobdet2_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatetobdet2" Enabled="true"></cc1:CalendarExtender>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkokbdet2" runat="server" CssClass="btn btn-primary" OnClick="lnkokbdet2_Click">Ok</asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <div class="msgHandSt">


                                                    <asp:UpdateProgress ID="UpdateProgress7" runat="server" DisplayAfter="50">
                                                        <ProgressTemplate>
                                                            <asp:Label ID="lblbdet2" runat="server" CssClass="lblProgressBar"
                                                                Text="Please Wait.........."></asp:Label>

                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>

                                                </div>

                                            </div>
                                        </div>

                                    </div>

                                    <asp:GridView ID="dgvBSdet" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="dgvBSdet_RowDataBound" Width="640px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcodebdet" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description Of accounts">
                                                <HeaderTemplate>
                                                    <table style="width: 47%;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label3122bdet" runat="server" Font-Bold="True"
                                                                    Text="Description Of Accounts" Width="180px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtnDetailsbsbdet" runat="server" BackColor="#000066"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center" Target="_blank" Width="90px">Next</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvBSDescbdet" runat="server" __designer:wfdid="w38"
                                                        CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                        Width="300px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvclobalbdet" runat="server" CssClass="GridLebel"
                                                        Font-Size="10px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvopnamtbdet" runat="server" CssClass="GridLebel"
                                                        Font-Size="10px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Changes During the Period"
                                                ItemStyle-HorizontalAlign="Right" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcuamtbdet" runat="server" CssClass="GridLebel"
                                                        Font-Size="10px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle  CssClass="grvRows" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </asp:View>
                        <asp:View ID="ViewISDetails02" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label21" runat="server" CssClass="control-label lblmargin-top9px" Text="From"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatefromisdet2" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDatefromisdet2_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefromisdet2" Enabled="true"></cc1:CalendarExtender>


                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label22" runat="server" CssClass="control-label lblmargin-top9px" Text="To"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDatetoisdet2" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDatetoisdet2_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatetoisdet2" Enabled="true"></cc1:CalendarExtender>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkokisdet2" runat="server" CssClass="btn btn-primary" OnClick="lnkokisdet2_Click">Ok</asp:LinkButton>

                                            </div>

                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="lblOpeningDate" runat="server" CssClass="control-label lblmargin-top9px" Text="From"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtOpeningDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtOpeningDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtOpeningDate" Enabled="true"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <div class="msgHandSt">


                                                    <asp:UpdateProgress ID="UpdateProgress8" runat="server" DisplayAfter="50">
                                                        <ProgressTemplate>
                                                            <asp:Label ID="lbllnkokisdet2" runat="server" CssClass="lblProgressBar"
                                                                Text="Please Wait.........."></asp:Label>

                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>

                                                </div>

                                            </div>
                                        </div>

                                    </div>

                                    <asp:GridView ID="dgvIS" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="dgvIS_RowDataBound" Width="647px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcode6" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                HeaderText="Description Of accounts">

                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvISDesc" runat="server" __designer:wfdid="w38"
                                                        CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                        Width="300px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Current Period"
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcuamt" runat="server" CssClass="GridLebel"
                                                        Font-Size="10px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Previous Period"
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel"
                                                        Font-Size="10px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvclobal" runat="server" CssClass="GridLebel"
                                                        Font-Size="10px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current %" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCPar" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percentcu")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total %" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPar" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpercent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
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


