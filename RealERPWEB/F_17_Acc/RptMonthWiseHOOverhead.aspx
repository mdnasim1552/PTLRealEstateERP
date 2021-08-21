<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMonthWiseHOOverhead.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptMonthWiseHOOverhead" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


      <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

           var gv1 = $('#<%=this.prjcost.ClientID %>');
            gv1.Scrollable();

            //gv1.gridviewScroll({

            //   width: 400,
            //   height: 420,
            //   arrowsize: 30,
            //    railsize: 16,
            //    barsize: 8,
            //    varrowtopimg: "../Image/arrowvt.png",
            //    varrowbottomimg: "../Image/arrowvb.png",
            //    harrowleftimg: "../Image/arrowhl.png",
            //    harrowrightimg: "../Image/arrowhr.png",
            //   freezesize: 6
         
            //});

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control  inputTxt" Style="width: 336px">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" Style="margin-left: -50px" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName">Date</asp:Label>


                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                        <asp:Label ID="lbldateto" runat="server" CssClass="lblTxt smLbl_to" Style="margin-right: 7px;">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                        
                                        
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" CssClass="rbtnList1"
                                            RepeatColumns="6"
                                            RepeatDirection="Horizontal" Style="text-align: left" >
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                            <asp:ListItem>Summary</asp:ListItem>
                                            
                                        </asp:RadioButtonList>

                                    </div>
                                    
                                </div>

                           
                                    

                               <%-- <div class="form-group" visible="false" >
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" Text="Size:" CssClass="lblName lblTxt"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage"
                                            TabIndex="4">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                           <asp:ListItem Value="300">500</asp:ListItem>
                                            <asp:ListItem Value="300">700</asp:ListItem>
                                            <asp:ListItem Value="300">900</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>
                                    
                                    
                                </div>--%>


                            </div>
                        </fieldset>
                    </div>

               <div class="row table-responsive">
                           <asp:GridView ID="prjcost" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False"
                                             ShowFooter="true" OnRowDataBound="prjcost_RowDataBound" >
                                           
                                            <RowStyle />
                                            <Columns>
                                               <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                           Style="font-family: Century Gothic, sans-serif; "
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                      <ItemStyle HorizontalAlign="right" Font-Names="Century Gothic" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                                </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvrptcode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptcode")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpactcode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>


                                         



                                       <asp:TemplateField HeaderText="Description" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvactDesc" runat="server"
                                                Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))+"</b>" %>'
                                                Width="150px" Font-Size="13px" ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Group Head" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProjDesc" runat="server"
                                            Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "mresdesc"))+"</b>" %>'
                                            Width="140px" Font-Size="13px" ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                    <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                </asp:TemplateField>
                                               
                                        
                                    <asp:TemplateField HeaderText="Particulars" >

                                    <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Particulars" Width="180px"></asp:Label>

                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>
                                    </asp:HyperLink>
                                </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvResDescd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                Width="180px" style="font-family: Century Gothic, sans-serif; "></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="amt1">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt1d" runat="server"   Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt2">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt2d" runat="server"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt3">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt3d" runat="server"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"/>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt4">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt4d" runat="server"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt5">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt5d" runat="server"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"/>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt6">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt6d" runat="server"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt7">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt7d" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt8">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt8d" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"/>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt9">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt9d" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt10">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt10d" runat="server"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt11">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt11d" runat="server"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt12">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt12d" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamttotal" runat="server"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"/>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
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
