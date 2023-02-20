<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptFixAsset02.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.RptFixAsset02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>
        .mt20 {
            margin-top: 20px;
        }
        .mt22 {
            margin-top: 21px;
        }
         .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
     </style>
    
    <script type="text/javascript">




        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });
        function pageLoaded() {
            <%-- var gv = $('#<%=this.dgvAccRec.ClientID %>');
            gv.Scrollable();--%>          

            var gvassetwise = $('#<%=this.gvassetwise.ClientID %>');

            gvassetwise.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 4
            });
            
            $('.chzn-select').chosen({ search_contains: true });
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
            <div class="card mt-3 mb-1">
                <div class="card-body p-1">
                    <div class="row">
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                 <asp:Label ID="lblDate" runat="server" CssClass="smLbl_to" Text="From"></asp:Label>
                                <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>
                                <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to">Report Type </asp:Label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"  TabIndex="6">
                                    <asp:ListItem Value="location" Selected="True" Text="LOCATION WISE"></asp:ListItem>
                                    <asp:ListItem Value="asset" Text="EQUIPMENT WISE"></asp:ListItem>
                                    <asp:ListItem Value="user" Text="USER WISE"></asp:ListItem>                                            
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2" id="divDept" Visible="false" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server" Visible="false" CssClass="lblTxt lblName">Department </asp:Label>
                                <asp:DropDownList ID="ddldeptName" Visible="false" runat="server" CssClass="form-control from-control-sm  chzn-select" TabIndex="6"></asp:DropDownList>                            
                            </div>
                        </div>                        
                        <div class="col-sm-2 col-md-2 col-lg-2" Visible="false" id="divUser" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblusr" runat="server" Visible="false" CssClass="lblTxt lblName">User </asp:Label>
                                <asp:DropDownList ID="ddluser" runat="server" Visible="false" CssClass="form-control from-control-sm chzn-select" TabIndex="6"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2" id="divAsset" Visible="false" runat="server">
                            <div class="form-group">
                                 <asp:Label ID="lblasst" runat="server" Visible="false" CssClass="lblTxt lblName">Asset </asp:Label>
                                 <asp:DropDownList ID="ddlasset" OnSelectedIndexChanged="ddlasset_SelectedIndexChanged" AutoPostBack="true" runat="server" Visible="false" CssClass="form-control from-control-sm chzn-select" TabIndex="6"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2" id="divAssetDetails" Visible="false" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblasstdet" runat="server" Visible="false"  CssClass=" lblTxt lblName">Asset Details </asp:Label>
                                <asp:DropDownList ID="ddlAssetDetails" runat="server" Visible="false"   CssClass="form-control from-control-sm chzn-select" TabIndex="6"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt22" OnClick="lbtnOk_Click" TabIndex="4">ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mb-1">
                <div class="card-body p-1" style="min-height:450px;">
                    <div class="row">
                        <div class="table-responsive">
                             <asp:GridView ID="gvFixAsset" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-bordered grvContentarea"  Width="461px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dept")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="Asset Name">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assetnam")) %>'
                                        Width="140px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Asset ID">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assetid")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "punit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                              

                                              
                            <asp:TemplateField HeaderText="Purchase Date" >
                                <ItemTemplate>
                                    <asp:Label ID="lblcode" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="100px"></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Rate">

                                    <FooterTemplate>
                                        <asp:Label ID="lgFrate7" runat="server" Font-Bold="True" Font-Size="13px"
                                             Style="text-align: right"> Total :</asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblrate"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Purchase Value">
                                  <FooterTemplate>
                                        <asp:Label ID="lgFPurValue" runat="server" Font-Bold="True" Font-Size="12px"
                                             Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblpurvalue"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pvalu")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                  <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Depreciation"> 
                                 <FooterTemplate>
                                        <asp:Label ID="lgFDepciation" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldepre"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depreamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                   <FooterStyle HorizontalAlign="Right" />
                                 <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Sale Value"> 
                                 <FooterTemplate>
                                        <asp:Label ID="lgsalval" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldepre"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                   <FooterStyle HorizontalAlign="Right" />
                                 <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                          <asp:TemplateField HeaderText="Book Value">
                              <FooterTemplate>
                                        <asp:Label ID="lgFBookVal" runat="server" Font-Bold="True" Font-Size="12px"
                                             Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblbookval"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                              <ItemStyle HorizontalAlign="Right"/>
                              <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "EMPNAME")) %>'
                                        Width="160px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             
                        </Columns>

                        <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                    </asp:GridView>
                    </div>                  

                    <div>
                        <div class="table-responsive">
                            <asp:GridView ID="gvassetwise" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="false" CssClass=" table-striped  table-bordered grvContentarea"  Width="461px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Asset Category">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc1")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Asset Name">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assetnam")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                               
                            
                             <asp:TemplateField HeaderText="Asset ID">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assetid")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "punit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dept")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

              

                               

                                              
                            <asp:TemplateField HeaderText="Purchase Date" >
                                <ItemTemplate>
                                    <asp:Label ID="lblcode" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Rate">
                                  <FooterTemplate>
                                        <asp:Label ID="lgFrate8" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"> Total :</asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblrate" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Purchase Value">
                                   <FooterTemplate>
                                        <asp:Label ID="lgFPurValue1" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblval"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pvalu")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Right" />
                                  <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="Depreciation">
                                 <FooterTemplate>
                                        <asp:Label ID="lgFDepciation1" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldepre"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depreamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Right" />
                                  <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Sale Value"> 
                                 <FooterTemplate>
                                        <asp:Label ID="lgsalval" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldepre"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                   <FooterStyle HorizontalAlign="Right" />
                                 <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            
                         <asp:TemplateField HeaderText="Book Value">
                             <FooterTemplate>
                                        <asp:Label ID="lgFBookVal1" runat="server" Font-Bold="True" Font-Size="12px"
                                           Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblbokval"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Right" />
                                  <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "EMPNAME")) %>'
                                        Width="160px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                    </asp:GridView>
                        </div>
                    </div>                     

                    <div class ="table-responsive"> 
                        <asp:GridView ID="gvuser" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-bordered grvContentarea"  Width="461px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "EMPNAME")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Asset Name">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assetnam")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Asset ID">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assetid")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "punit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dept")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                
                            <asp:TemplateField HeaderText="Purchase Date" >
                                <ItemTemplate>
                                    <asp:Label ID="lblcode" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblrate2" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                   <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Purchase Value">
                                    <FooterTemplate>
                                        <asp:Label ID="lgFPurValue2" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblpurval" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pvalu")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                   <ItemStyle HorizontalAlign="Right" />
                                  <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                    

                              <asp:TemplateField HeaderText="Depreciation">
                                   <FooterTemplate>
                                        <asp:Label ID="lgFDepreciation2" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldepre"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depreamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                  <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            
                              <asp:TemplateField HeaderText="Sale Value"> 
                                 <FooterTemplate>
                                        <asp:Label ID="lgsalval" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldepre"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                   <FooterStyle HorizontalAlign="Right" />
                                 <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Book value">
                                       <FooterTemplate>
                                        <asp:Label ID="lgFBookVal2" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblbookval"  runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                  <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                        </Columns>

                        <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                    </asp:GridView>
                    </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



