<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMonthlyProbCollection.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptMonthlyProbCollection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 300px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 300px !important;
        }

        .multiselect-container {
            height: 350px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 300px !important;
        }

        .form-control {
            height: 34px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
          <%--  $('#<%=this.gvsupstatus.ClientID%>').tblScrollable();--%>
            $(function () {
                $('[id*=chkSupCategory').multiselect({
                    includeSelectAllOption: true,

                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,

                });
                var gvprobacoll = $('#<%=this.gvprobacoll.ClientID %>');

                gvprobacoll.Scrollable();

            });

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
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">From Date</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control flatpickr-input" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">To Date</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control flatpickr-input" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="from-group">
                                <label class="control-label">Project Name</label>
                                <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>

                         <div class="col-md-2">
                            <div class="from-group">
                                <label class="control-label">Followed By</label>
                                <asp:DropDownList ID="ddlSalesperson" runat="server" CssClass="chzn-select form-control  inputTxt" Width="200px" AutoPostBack="True"></asp:DropDownList>
                            
                              
                         
                       
                         
                            </div>
                        </div>
                     
                         <div class="col-md-2" >
                            
                             <asp:Label ID="Label1" runat="server"> Type</asp:Label>
                            <asp:RadioButtonList ID="rbtnAtStatus" runat="server" AutoPostBack="True"  Style="border-radius: 5px; padding: 0 5px;"
                                CssClass="custom-control custom-control-inline custom-checkbox rbtnAtStatus d-block p-0 mt-3"
                                Font-Bold="True" Font-Size="12px" ForeColor="Black" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">&nbsp;Probable&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem>&nbsp; All Dues</asp:ListItem>

                            </asp:RadioButtonList>

                        </div>
                           


                        
                        


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lnkbtnOk_Click" AutoPostBack="True">Ok</asp:LinkButton>
                            </div>
                        </div>




                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 250px;">
                <div class="card-body">
               

                            <asp:GridView ID="gvprobacoll" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprjname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcustname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                               

                                    

                                    <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunitname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                               <asp:TemplateField HeaderText="Mobile No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmobile" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobileno")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    

                                    <asp:TemplateField HeaderText="DOP">
                                        <FooterTemplate>
                                                    <asp:Label ID="lblFgvpaydat" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right">Total :</asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpaydat" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy")%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                                                    
                                   <asp:TemplateField HeaderText="Installment</br>Payable Amount">
                                        <FooterTemplate>
                                                    <asp:Label ID="lblFamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                         </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdueamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" /> 
                                        <FooterStyle HorizontalAlign="right" />

                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Followed By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfollowby" runat="server" CssClass="word-break: break-all"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "steam")) %>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                                                                                            
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>


                    

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
