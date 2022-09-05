<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="TimeOfLeave.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.TimeOfLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
                .chzn-drop {
            width: 100% !important;
        }
        .chzn-container{
            width: 100% !important;
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
            function TimeOffModal() {
                $('#TimeOffModal').modal('toggle');
            }

            function CloseModal() {
                $('#TimeOffModal').modal('hide')
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


            <div class="card-fluid container-data  mt-2">
                <div class="row" id="divError" runat="server" visible="false">
                    <div class="col-12 col-lg-12 col-xl-12">
                        <div class="section-block">


                            <div class="alert alert-danger has-icon" role="alert">
                                <div class="alert-icon">
                                    <span class="fa fa-bullhorn"></span>
                                </div>
                               <span id="spnErrorTxt" runat="server"></span>

                            </div>



                        </div>
                    </div>
                </div>
                <div class="row" id="warning" runat="server" visible="false">
                    <div class="col-12 col-lg-12 col-xl-12">
                        <div class="section-block">


                            <div class="alert alert-danger has-icon" role="alert">
                                <div class="alert-icon">
                                    <span class="fa fa-bullhorn"></span>
                                </div>
                                The supervisor setup incomplete
                                <br />

                                Please update your supervisor  
                                <asp:HyperLink ID="hylnkUserProfileEdit" class="alert-link" runat="server" NavigateUrl="~/F_81_Hrm/F_82_App/EmpProfileEdit.aspx" Target="_blank" ToolTip="Edit Your Profile"><i class="fas fa-user-edit">&nbsp;Click</i></asp:HyperLink>



                            </div>



                        </div>
                    </div>
                </div>

            </div>

            <div class="row" id="Lvform" runat="server"  >
                <div class="col-12 col-lg-12 col-xl-3">
                    <section class="card card-fluid" style="min-height: 650px">
                        <header class="card-header">Application for Time Of Leave</header>
                        <div class="card-body" id="ApplicFrm" runat="server">


                                      <div class="form-group" id="empMgt" runat="server" visible="false">
                                    <label for="Employee">Employee </label> 
                                    <asp:DropDownList ID="ddlEmpName" runat="server" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged"
                                        CssClass="chzn-select form-control" TabIndex="2" AutoPostBack="true">
                                    </asp:DropDownList>

                                </div>
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Date   
                                </label>
                                <asp:TextBox ID="txtaplydate" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                    TargetControlID="txtaplydate"></cc1:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Remaning Time   
                                </label>
                                <asp:TextBox ID="txtTimeLVRem" runat="server" ReadOnly="true" AutoPostBack="true" class="form-control bg-green" Font-Bold="false" ForeColor="White"></asp:TextBox>

                            </div>
                            <div class="row">
                                <div class="col-md-6 pl-0">
                                    <div class="form-group">
                                        <label for="sel1" id="frmdate" runat="server">From Time <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtFromTime" runat="server" TextMode="Time"  OnTextChanged="txtFromTime_TextChanged" AutoPostBack="true" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-6 pl-0">
                                    <div class="form-group">
                                        <label for="sel1" id="Label1" runat="server">To Time <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtToTime" runat="server" TextMode="Time"    AutoPostBack="true" OnTextChanged="txtToTime_TextChanged" class="form-control"></asp:TextBox>

                                       

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Use Time   (hh:mm)
                                </label>

                                <asp:TextBox ID="txtUseTime" runat="server" ReadOnly="true" AutoPostBack="true" class="form-control bg-danger" ForeColor="White"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label for="Reason">Reason/Remarks</label>
                                <asp:TextBox ID="txtLeavLreasons" runat="server" placeholder="Reason" TextMode="MultiLine" class="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group text-right">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" CssClass="btn btn-sm btn-primary" />
                            </div>

                        </div>
                    </section>
                </div>

                <div class="col-12 col-lg-12 col-xl-9">
                    <section class="card card-fluid mb-0" style="min-height: 650px; flex-grow: 1; overflow: auto;">
                        <header class="card-header">Time of Leave History</header>

                        <div class="card-body">
                            <strong>Current month history</strong>
                         <asp:GridView ID="gvLvReq" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered"
                                                        ShowFooter="True" OnRowDataBound="gvLvReq_RowDataBound">
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

                                                            <asp:TemplateField HeaderText="leaveIdd" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbllevid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                                                                               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>                                                             
                                                           
                                                            <asp:TemplateField HeaderText="Apply Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Out Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intime")) %>'
                                                                        Width="80px"></asp:Label>
                                                                   



                                                                </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="In Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "outtime")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterTemplate>
                                                                    Total
                                                                </FooterTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Use Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDuration" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "USETIME")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotalremtime" runat="server" CssClass="badge bg-danger text-white"></asp:Label>

                                                                   
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remaning Time" ControlStyle-BackColor="#ccffcc">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvremtime" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remaintime")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Reason/Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReason"  ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>' runat="server" 
                                                                        Height="20px" Text='<%# Eval("remarks").ToString().Length>50 ? (Eval("remarks") as string).Substring(0,25)+ "....."
                                                                             : Eval("remarks")  %>'></asp:Label>

                                                                                                                                
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Current Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvCust" runat="server" BackColor="Transparent"  
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqstatus")) %>'
                                                                        ></asp:Label>                                                                    
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>                                                                    
                                                                  
                                                                    
                                                                    <asp:LinkButton ID="lkDelete" runat="server" ForeColor="Red"  
                                                                        OnClientClick="retun confirm();"
                                                                       OnClick="lkDelete_Click"
                                                                          Visible='<%# Eval("isApprve").ToString() == "True" ? false : true %>'
                                                                         
                                                                         CssClass="btn btn-xs btn-default" ><span class="fa fa-trash"></span></asp:LinkButton>
                                                                    
                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" HorizontalAlign="left" />

                                                                <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                          <asp:LinkButton ID="lnkTimeEdit" OnClick="lnkTimeEdit_Click" CssClass="text-info" runat="server" ToolTip="Edit Time"><i class="fa fa-edit"></i></asp:LinkButton>
                                                  
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        
                                                    </asp:GridView>
                           <hr />
                            <strong>All  history</strong>
                            <asp:GridView ID="gvLvReqAll" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered"
                                                        ShowFooter="True" >
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2SlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="leaveIdd" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2id" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2empid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                                                                               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>                                                             
                                                           
                                                            <asp:TemplateField HeaderText="Apply Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvapplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Out Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2intime" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intime")) %>'
                                                                        Width="80px"></asp:Label>
                                                                   



                                                                </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="In Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvouttime" runat="server" BackColor="Transparent"
                                                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "outtime")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterTemplate>
                                                                    Total
                                                                </FooterTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Use Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2duration" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "USETIME")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotalremtime" runat="server" CssClass="badge bg-danger text-white"></asp:Label>

                                                                   
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remaning Time" ControlStyle-BackColor="#ccffcc">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvremtime" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remaintime")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Reason/Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReason"  ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>' runat="server" 
                                                                        Height="20px" Text='<%# Eval("remarks").ToString().Length>50 ? (Eval("remarks") as string).Substring(0,25)+ "....."
                                                                             : Eval("remarks")  %>'></asp:Label>

                                                                                                                                
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Current Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvCust" runat="server" BackColor="Transparent"  
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqstatus")) %>'
                                                                        ></asp:Label>                                                                    
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                     
                                                        </Columns>
                                                        
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        
                                                    </asp:GridView>  
                        </div>
                    </section>
                </div>
            </div>

                   <!-- Modal -->
        <div class="modal fade" id="TimeOffModal" tabindex="-1" role="dialog" aria-labelledby="NoticeModalCenterTitle" aria-hidden="true" data-keyboard="false" data-backdrop="static" oncontextmenu="return false;">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header order-bottom bg-info mb-3">
                         <p class="modal-title font-weight-bold text-white" id="">Edit Time Off</p>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            x
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="card">
                       <%--     <div class="card-header bg-info ">
                               <h6 class="modal-title font-weight-bold text-white p-0 m-0" id="">Edit Time Off</h6>
                            </div>--%>
                            <div class="card-body bg-light">
                                <span runat="server" id="useTime" visible="false"></span>

                                <span runat="server" id="timeOfId" visible="false"></span>
                                   <span runat="server" id="applydatmodal" visible="false"></span>
                           
                                             <div class="form-group">
                                        <label for="txtmodalouttime" id="lblmodalouttime" runat="server">Out Time <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtmodalouttime" runat="server" TextMode="Time"  class="form-control" ></asp:TextBox>
                                    </div>

                                      <div class="form-group">
                                        <label for="txtmodalintime" id="lblmodalintime" runat="server">In Time <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtmodalintime" runat="server" TextMode="Time"  class="form-control" ></asp:TextBox>
                                    </div>

                                <asp:LinkButton runat="server" ID="lnkTimeUpdate_Click" OnClick="lnkTimeUpdate_Click_Click" CssClass="btn btn-primary btn-sm mb-2">Update</asp:LinkButton>
                            </div>
                        </div>




                    </div>
                </div>
            </div>
        </div>

        </ContentTemplate>

        
    </asp:UpdatePanel>
</asp:Content>
