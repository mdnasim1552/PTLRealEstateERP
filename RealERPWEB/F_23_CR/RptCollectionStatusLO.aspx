<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptCollectionStatusLO.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptCollectionStatusLO" %>
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
                var gvcollStatus = $('#<%=this.gvcollStatus.ClientID %>');

                gvcollStatus.Scrollable();

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
                       <%-- <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">From Date</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control flatpickr-input" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>--%>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ToDate"> Date :</label>
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
                                <label class="control-label">Beneficiary</label>
                                <asp:DropDownList ID="ddlbenefname" runat="server" CssClass="chzn-select form-control  inputTxt" Width="350px" AutoPostBack="True"></asp:DropDownList>
                            
                              
                         
                       
                         
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lnkbtnOk_Click" AutoPostBack="True">Ok</asp:LinkButton>
                            </div>
                        </div>




                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
               

                            <asp:GridView ID="gvcollStatus" runat="server" AutoGenerateColumns="False"
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

                                    <asp:TemplateField HeaderText="Beneficiary Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbenename" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "benefnamedesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cheque No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvchq" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
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

                               <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbankname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    

                                    <asp:TemplateField HeaderText="Date">
                                        <FooterTemplate>
                                                    <asp:Label ID="lblFgvpaydat" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right">Total :</asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpaydat" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy")%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                                                    
                                   <asp:TemplateField HeaderText="Paid Amount">
                                        <FooterTemplate>
                                                    <asp:Label ID="lblFamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                         </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" /> 
                                        <FooterStyle HorizontalAlign="right" />

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
