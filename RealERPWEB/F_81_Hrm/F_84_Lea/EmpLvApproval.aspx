<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpLvApproval.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.EmpLvApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        };


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
    </style>



    <div class="container moduleItemWrpper">
        <div class="contentPart">
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
                    <div class="row">
                        <div class="col-md-7 pading5px">
                            <fieldset class="scheduler-border fieldset_A">
                                <asp:Panel ID="PnlSubCon" runat="server">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-7 pading5px  ">


                                                <asp:Label ID="lbldate" runat="server" CssClass=" lblTxt lblName"> Date</asp:Label>
                                                <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inputName inPixedWidth120 "></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                                <asp:Label ID="lblmrfno" runat="server" CssClass="smLbl" Style="padding-left: 12px;">Ref No</asp:Label>
                                                <asp:TextBox ID="txtserchmrf" runat="server" CssClass="inputTxt inputName" Width="60"></asp:TextBox>


                                            </div>
                                            <div class="col-md-2 pading5px">
                                                <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>

                                            </div>




                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblProject" runat="server" CssClass="lblTxt lblName">Center Name</asp:Label>
                                                <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="ImgbtnFindProjectName" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindProjectName_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="col-md-5 pading5px">
                                                <asp:DropDownList ID="ddlCenter" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-3 pull-right">
                                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        
                                    </div>







                                </asp:Panel>
                            </fieldset>
                        </div>

                        <div class="col-md-5 pading5px">
                            <asp:Panel ID="Panel2" runat="server">

                                <asp:ListBox ID="lstOrderNo" runat="server" AutoPostBack="True"
                                    BackColor="#DFF0D8" Font-Bold="True" Font-Size="12px" Height="120px"
                                    OnSelectedIndexChanged="lstVouname_SelectedIndexChanged"
                                    SelectionMode="Multiple" Style="margin-left: 12px" TabIndex="12" Width="500px"></asp:ListBox>
                            </asp:Panel>

                        </div>
                    </div>


                    <div class="row">


                        <asp:Label ID="lblLvInfo" runat="server" CssClass=" btn btn-success primaryBtn" Text="Leave Information"></asp:Label>
                        <div class="clearfix"></div>

                        <asp:GridView ID="gvLvReq" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="gcode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgcod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Emp Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID Card">
                                    <ItemTemplate>
                                        <asp:Label ID="lgidcard" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Width="80px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgdeptanme" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                            Width="120px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgdesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="120px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lglvtype" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                            Width="80px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                               

                                <asp:TemplateField HeaderText="Apply Date">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotal" runat="server"  CssClass="btn  btn-primary primarygrdBtn"  OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                            </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Applied For">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvlapplied" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" BackColor="Transparent" Font-Size="12px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                           
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvlstdate" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvlstdate_CalendarExtender" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtgvlstdate">
                                                </cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                               
                                <asp:TemplateField HeaderText="End Date">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
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
                    
                     <div class="col-md-4 pull-right">
                                                  <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-primary ApprovedBtn" OnClick="lbtnDelete_Click" BorderStyle="None" >Not Approved</asp:LinkButton>
                                                <asp:LinkButton ID="ApprovedBtn" runat="server" CssClass="btn btn-primary ApprovedBtn" OnClick="ApprovedBtn_Click" BorderStyle="None" >Approved</asp:LinkButton>
                      <%--     <asp:LinkButton ID="ApprovedBtn" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="#000" CssClass=" btn btn-success primaryBtn"
                                    Text="Approved" Visible="true"></asp:LinkButton>--%>
                                          
                         
                         <label id="chkbod" runat="server" class="switch">
                            <asp:CheckBox ID="Chboxforward" runat="server" />
                            <span class="btn btn-xs slider round"></span>
                        </label>
                        <asp:Label  id="lblforward" runat="server" Text="Forward" CssClass="btn btn-xs"></asp:Label>
                         
                         
                         
                           </div>


                    <div class="row">

                         <asp:Label ID="lblleaveStatus" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="#000" CssClass=" btn btn-success primaryBtn"
                                    Text="Leave Status" Visible="False"></asp:Label>
                        <div class="clearfix"></div>
                                <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="925px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entitlement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave This Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requested">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Requested  Std. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlrequeststdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lrstrtdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lrstrtdat")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Std. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoydt10" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>




                    <asp:Panel ID="PnlNarration" runat="server" Visible="false">

                        <fieldset class="scheduler-border fieldset_Nar">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="lblNarration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="lblvalNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 pading5px">
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Approved By:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="lblRemarks" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="lblremark" runat="server" CssClass="lblTxt lblName" Text="Remaks:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtremarks" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                            </div>

                        </fieldset>



                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%-- <div class="row">
                <div class="col-sm-7 col-md-7 col-lg-7">
                    <div class="formrow" style="margin: 5px 0px;">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btngl pull-left" TabIndex="11" Text="Historical Price"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btngl pull-left" TabIndex="11" Text="Ledger"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btngl pull-left" TabIndex="11" Text="Transaction List"></asp:LinkButton>
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="col-sm-5 col-md-5 col-lg-5">
                    <div class="formrow" style="margin: 5px 0px;">
                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="btnnew " Style="margin: 0 5px;" OnClick="lnkbtnEdit_Click"><span class="flaticon-edit26"></span> Edit</asp:LinkButton>
                        <asp:LinkButton ID="btnClose" runat="server" CssClass="btnclose btnc pull-right" OnClick="btnClose_Click" TabIndex="11" Text="Close"></asp:LinkButton>
                        <asp:LinkButton ID="btnUpdate" runat="server" OnClientClick="return validateLimit();" CssClass="btnsave pull-right" TabIndex="11" Text="Save A/C" OnClick="btnUpdate_Click"></asp:LinkButton>

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>--%>
        </div>
    </div>


</asp:Content>
