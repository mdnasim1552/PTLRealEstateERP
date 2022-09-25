<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSoldUnsoldUnitAvgPrice.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptSoldUnsoldUnitAvgPrice" %>
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

 <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            $(".select2").select2();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });




        function pageLoaded() {
            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
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
                        <%--<div class="col-md-2">
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
                        </div>--%>
                        <div class="col-md-3">
                            <div class="from-group">
                                <label class="control-label">Project Name</label>
                                <asp:ListBox ID="chkProjectName" runat="server" ClientIDMode="Static" CssClass="form-control select2" Style="min-width: 200px !important;" SelectionMode="Multiple"></asp:ListBox>
                               <%-- <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True" ></asp:DropDownList>--%>
                            </div>
                        </div>


                         

                        

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate"> Date</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control flatpickr-input" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lnkbtnOk_Click" AutoPostBack="True">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2" >
                            
                             <asp:Label ID="Label1" runat="server" Font-Size="12px">Type</asp:Label>
                            <asp:RadioButtonList ID="rbtnStatus" runat="server" AutoPostBack="True"  Style="border-radius: 5px; padding: 0 5px;"
                                CssClass="custom-control custom-control-inline custom-checkbox rbtnAtStatus d-block p-0 mt-3"
                                Font-Bold="True" Font-Size="12px" ForeColor="Black" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem  Value="Sold">&nbsp; Sold &nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="Unsold">&nbsp; Unsold</asp:ListItem>
                                <asp:ListItem Selected="True">&nbsp; both</asp:ListItem>


                            </asp:RadioButtonList>

                        </div>



                    </div>
                </div>
            </div>
           

            <div class="card card-fluid">
                  <div class="table-responsive">
                <div class="card-body">
               

                            <asp:GridView ID="gvsoldunsoldavg" runat="server" AutoGenerateColumns="False"
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
                                        
                                       <FooterTemplate>
                                                    <asp:Label ID="lblFprjdesc" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprjdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Type">
                                        
                                       <FooterTemplate>
                                                    <asp:Label ID="lblFflr" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvflr" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Saleable Unit </br> Apt/</br>Shop/</br>comm.">

                                       <FooterTemplate>
                                                    <asp:Label ID="lblFsalableunit" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" ></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsalableunit" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                   <%--   <asp:TemplateField HeaderText="Total Salable <br> Area">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFtotalsize" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtotalsize" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tusize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>

                                     <asp:TemplateField HeaderText="Sold</br> Unit">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFsoldunit" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsoldunit" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sold Area">

                                        <FooterTemplate>
                                                    <asp:Label ID="lblFsoldarea" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsoldarea" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "susize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="UnSold </br> Unit">
                                         <FooterTemplate>
                                                    <asp:Label ID="lblFunsoldunit" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunsoldunit" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    


                                        <asp:TemplateField HeaderText="UnSold Area">
                                            <FooterTemplate>
                                                    <asp:Label ID="lblFgvunsoldarea" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunsoldarea" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="UnSold Amount">
                                            <FooterTemplate>
                                                    <asp:Label ID="lblFgvunsoldamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunsoldamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Total Value">
                                          <FooterTemplate>
                                                    <asp:Label ID="lblFgvtsoldamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtsoldamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Collection">
                                         <FooterTemplate>
                                                    <asp:Label ID="lblFgvreceived" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreceived" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Recievable">
                                         <FooterTemplate>
                                                    <asp:Label ID="lblFgvrecievable" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrecievable" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recievable")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    
                                     <asp:TemplateField HeaderText="Per SFT Rate </br>from sold units </br> (Except Car Park & Utilities)">
                                         <FooterTemplate>
                                                    <asp:Label ID="lblFgvsunitexpcarutility" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsunitexpcarutility" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "expcarutilyrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Total Value </br> (Inc. Apt./Shop/Comm.</br> Cost + Car Parking + Utility)">
                                         <FooterTemplate>
                                                    <asp:Label ID="lblFgincvsunitexpcarutility" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvincsunitexpcarutility" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctwcarutility")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    

                                    

                                    <asp:TemplateField HeaderText="Per Sft Rate </br> from sold units </br> (Inc. Apt. + Car Parking + Utility)">
                                         <FooterTemplate>
                                                    <asp:Label ID="lblFgvsunitincparunitiyrate" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsunitincparunitiyrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Inccarutilyrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Average Per Sft </br>Rate from unsold</br> unit(Except Car Park & Utilities)">
                                        <FooterTemplate>
                                                    <asp:Label ID="lblFgvunsolduitrate" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunsolduitrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "expuscarutilityrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount receivable </br> from unsold unit </br>(Inc. Apt./Shop/Comm. Cost + Car Parking + Utilities))">

                                          <FooterTemplate>
                                                    <asp:Label ID="lblFgvunsoldreaceble" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunsoldreaceble" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incunsoldcarutility")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Average Per Sft </br>Rate from unsold unit </br>(Inc. Apt./Shop/Cost + Car Parking + Utilities))">
                                        <FooterTemplate>
                                                    <asp:Label ID="lblFgvunsoldwparkingutiltyrat" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunsoldwparkingutiltyrat" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incunsoldavgrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Right"/>
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
            </div>
             
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>